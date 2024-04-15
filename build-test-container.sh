#!/bin/bash

tar -czf ./build-files.tar.gz ./*.sln ./src/*/src/*/src/*.csproj
docker compose -f ./srm.test.docker-compose.yml -p sunraysmarket up -d
