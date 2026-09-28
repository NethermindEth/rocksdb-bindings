# SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
# SPDX-License-Identifier: MIT

file(WRITE "${OUTPUT}" "Licenses of the statically linked Linux dependencies, including Folly.\nGenerated from the pinned vcpkg build.\n")
file(GLOB licenses "${DEPS}/share/*/copyright")
list(SORT licenses)
foreach(license IN LISTS licenses)
  get_filename_component(directory "${license}" DIRECTORY)
  get_filename_component(port "${directory}" NAME)
  file(READ "${license}" text)
  file(APPEND "${OUTPUT}" "\n\n${port}\n========================================\n${text}\n")
endforeach()
