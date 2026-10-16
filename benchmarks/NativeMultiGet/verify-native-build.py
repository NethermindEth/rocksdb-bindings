import json
import os
import re
import subprocess
import sys
import urllib.request
from pathlib import Path

candidate, arch, run_id, output = sys.argv[1:]
candidate = Path(candidate)
output = Path(output)
if arch not in ("x64", "arm64") or not re.fullmatch(r"[0-9]+", run_id):
    raise SystemExit("Invalid native build run or architecture")
base = os.environ["GITHUB_API_URL"] + "/repos/" + os.environ["GITHUB_REPOSITORY"]


def read_api(path):
    request = urllib.request.Request(base + path, headers={
        "Authorization": "Bearer " + os.environ["GH_TOKEN"],
        "Accept": "application/vnd.github+json",
    })
    with urllib.request.urlopen(request, timeout=30) as response:
        return json.load(response)


run = read_api(f"/actions/runs/{run_id}")
if run["head_repository"]["full_name"] != os.environ["GITHUB_REPOSITORY"]:
    raise SystemExit("Native artifacts came from a different repository")
if run["path"] != ".github/workflows/build-rocksdb.yml":
    raise SystemExit("Native artifacts came from a different build workflow")
sha = run["head_sha"]
if not re.fullmatch(r"[0-9a-f]{40}", sha):
    raise SystemExit("Invalid native build revision")
jobs = read_api(f"/actions/runs/{run_id}/jobs?per_page=100")["jobs"]
job = next((job for job in jobs if job["name"] == f"Linux build ({arch})"), None)
if job is None:
    raise SystemExit("Native build job is missing")
for name in ("Build", "Verify", "Upload artifact"):
    if not any(step["name"] == name and step["conclusion"] == "success" for step in job["steps"]):
        raise SystemExit(f"Native build step did not pass: {name}")
paths = (
    "native/CMakeLists.txt", "native/apply.cmake",
    "native/rocksdb_batch.inc", "native/rocksdb_batch.h",
    "native/rocksdb_async.inc", "native/rocksdb_async.h",
    ".github/workflows/build-rocksdb.yml",
)
for path in paths:
    built = subprocess.check_output(["git", "-C", str(candidate), "show", f"{sha}:{path}"])
    if built != (candidate / path).read_bytes():
        raise SystemExit(f"Native artifact sources changed: {path}")
workflow_path = ".github/workflows/build-rocksdb.yml"
built_workflow = subprocess.check_output([
    "git", "-C", str(candidate), "show", f"{sha}:{workflow_path}",
], text=True)
current_workflow = (candidate / workflow_path).read_text()
if built_workflow != current_workflow:
    raise SystemExit("Native build workflow changed")
for name in ("Linux build (arm64)", "Linux build (x64)", "macOS build (arm64)", "macOS build (x64)", "Windows build (arm64)", "Windows build (x64)"):
    built_job = next((entry for entry in jobs if entry["name"] == name), None)
    if built_job is None or built_job["conclusion"] != "success":
        raise SystemExit(f"Native build did not pass: {name}")
if run["conclusion"] != "success":
    raise SystemExit("Native build run did not pass")
(output / "native-build-run.json").write_text(json.dumps(run, indent=2) + "\n")
(output / "native-build-job.json").write_text(json.dumps(job, indent=2) + "\n")
(output / "native-build-sha.txt").write_text(sha + "\n")
(output / "build-workflow.yml").write_text(built_workflow)
print(f"Verified native artifact build {run_id} at {sha}: sources, commands, build checks and architecture match")
