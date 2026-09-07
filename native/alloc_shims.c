// Routes the libc calls whose result RocksDB frees itself onto the bundled allocator.
//
// librocksdb.so bundles an unprefixed jemalloc, so every malloc and free inside the library binds
// to it. glibc still allocates from its own heap, and a few of its functions hand that memory to
// the caller to free: strdup (the C API's SaveError), getline (PosixHelper reads sysfs on every
// DB::Open), and getcwd/realpath when passed a NULL buffer. Each is linked with -Wl,--wrap so the
// reference lands here, where the result comes from the bundled heap and no glibc-owned pointer
// ever reaches a RocksDB free().
#define _GNU_SOURCE
#include <errno.h>
#include <limits.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

extern char *__real_getcwd(char *buf, size_t size);
extern char *__real_realpath(const char *path, char *resolved);

char *__wrap_strdup(const char *s)
{
  size_t n = strlen(s) + 1;
  char *p = malloc(n);
  return p ? memcpy(p, s, n) : NULL;
}

// A NULL buffer asks glibc to allocate one: size bytes, or as much as needed when size is 0.
// Allocate that buffer here instead and let glibc fill it.
char *__wrap_getcwd(char *buf, size_t size)
{
  if (buf) return __real_getcwd(buf, size);
  size_t n = size ? size : 256;
  for (;;) {
    char *p = malloc(n);
    if (!p) return NULL;
    if (__real_getcwd(p, n)) return p;
    int error = errno;
    free(p);
    if (size || error != ERANGE) {
      errno = error;
      return NULL;
    }
    n *= 2;
  }
}

// With a caller-supplied buffer realpath requires PATH_MAX bytes, so that is what a NULL request
// becomes.
char *__wrap_realpath(const char *path, char *resolved)
{
  if (resolved) return __real_realpath(path, resolved);
  char *p = malloc(PATH_MAX);
  if (!p) return NULL;
  if (__real_realpath(path, p)) return p;
  int error = errno;
  free(p);
  errno = error;
  return NULL;
}

// getline grows the caller's buffer with realloc, so the whole routine has to live on the bundled
// heap; glibc's version is never called.
ssize_t __wrap___getdelim(char **lineptr, size_t *n, int delim, FILE *stream)
{
  if (!lineptr || !n || !stream) {
    errno = EINVAL;
    return -1;
  }
  if (!*lineptr || *n == 0) {
    char *buffer = realloc(*lineptr, 128);
    if (!buffer) return -1;
    *lineptr = buffer;
    *n = 128;
  }
  size_t length = 0;
  int c = EOF;
  while ((c = getc(stream)) != EOF) {
    if (length + 2 > *n) {
      size_t grown = *n * 2;
      char *buffer = realloc(*lineptr, grown);
      if (!buffer) return -1;
      *lineptr = buffer;
      *n = grown;
    }
    (*lineptr)[length++] = (char)c;
    if (c == delim) break;
  }
  if (c == EOF && (length == 0 || ferror(stream))) return -1;
  (*lineptr)[length] = '\0';
  return (ssize_t)length;
}

ssize_t __wrap_getdelim(char **lineptr, size_t *n, int delim, FILE *stream)
{
  return __wrap___getdelim(lineptr, n, delim, stream);
}

ssize_t __wrap_getline(char **lineptr, size_t *n, FILE *stream)
{
  return __wrap___getdelim(lineptr, n, '\n', stream);
}
