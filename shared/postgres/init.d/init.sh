#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" <<-EOSQL
	CREATE USER $DB_USER WITH PASSWORD '$DB_USER_PASSWORD';

	CREATE DATABASE appointments;
	ALTER DATABASE appointments OWNER TO $DB_USER;
	GRANT ALL PRIVILEGES ON DATABASE appointments TO $DB_USER;
	GRANT USAGE, CREATE ON SCHEMA public TO $DB_USER;
EOSQL