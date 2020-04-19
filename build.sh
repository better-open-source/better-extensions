#!/usr/bin/env bash

docker build . -t better-extensions-build-image -f Dockerfile --build-arg CI_BUILDID=$1 --build-arg CI_PRERELEASE=$2
docker create --name better-extensions-build-container better-extensions-build-image
docker cp better-extensions-build-container:./app/out ./out
docker rm -fv better-extensions-build-container
