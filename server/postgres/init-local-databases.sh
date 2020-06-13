#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    CREATE DATABASE "nccp-local";
    CREATE DATABASE "nccp-testing";
    GRANT ALL PRIVILEGES ON DATABASE "nccp-local" TO postgres;
    GRANT ALL PRIVILEGES ON DATABASE "nccp-testing" TO postgres;
EOSQL
