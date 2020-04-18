#!/usr/bin/env bash

docker build . -t better-extensions-build-image -f Dockerfile
docker create --name better-extensions-build-container better-extensions-build-image
docker cp better-extensions-build-container:./app/out ./out
docker rm -fv better-extensions-build-container
