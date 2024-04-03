#!/usr/bin/env bash
set -e

source ./scripts/functions.sh

ENDLINE='\n'

ADMIN_PASSWORD='Pa$$word123'
ADMIN_CLIENT_SECRET="$(gen_random_string 64)"

source .env

cat <<EOF | tee .env
ADMIN_PASSWORD="$ADMIN_PASSWORD"
ADMIN_CLIENT_SECRET="$ADMIN_CLIENT_SECRET"
EOF