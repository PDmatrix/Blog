#!/bin/bash

export TS_POST_PROCESS_FILE="$(pwd)/../node_modules/.bin/prettier --write"
openapi-generator generate -i swagger.json -g typescript-axios --enable-post-process-file
sed -i "/<any>/d" ./api.ts