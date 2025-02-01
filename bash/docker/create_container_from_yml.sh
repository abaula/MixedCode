#!/bin/bash

YML_FILE_PATH=$1

docker-compose -f $YML_FILE_PATH up --no-start

