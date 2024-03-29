#!/usr/bin/env bash

source .env

cat <<EOF | dotnet user-secrets set --id '1c6837c9-a327-43b0-9139-f864c65c0c95'
{
  "OfficesDb": {
    "Prefix": "mongodb://",
    "Host": "localhost",
    "Port": "27017",
    "User": "officesAPI",
    "Password": "$OFFICES_DB_PASSWORD",
    "Database": "offices"
  }
}
EOF