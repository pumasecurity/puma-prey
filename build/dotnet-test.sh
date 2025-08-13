#!/bin/bash
set -e

# set local vars
TEST_DIRECTORY=$1
TEST_FILE=$2

# change to the test directory
cd "$TEST_DIRECTORY"
dotnet restore
dotnet build
dotnet test --no-restore --no-build --logger "trx;LogFileName=${TEST_FILE}" || true
