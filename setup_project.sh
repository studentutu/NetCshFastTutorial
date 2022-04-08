#!/usr/bin/env sh

# helper here https://github.com/chubin/cheat.sheets

# breaks exectuion of scenario if error is encountered
set -e

# prints commands as they are executed
set -x 
# set +x

# use it with cmd : source ./shellScript.sh
export BACK_END_BASE_URI='https://localhost:7076'


# use echo 
echo BACK_END_BASE_URI

# requires git on machine
git config --global core.ignorecase false
git config --global core.longpaths true

# make sure lfs is enabled, but also skip failures on lfs fetch
git lfs install --skip-smudge


# add dev cert to env, trust developer certifiactes, if this fails -> install Rider and install .Net support
dotnet dev-cert https --trust