#!/bin/bash

docker build -t pyhello -f DOCKERFILE .

docker rm py

docker run --name py pyhello
