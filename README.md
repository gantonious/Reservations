# Reservations [![CircleCI](https://circleci.com/gh/gantonious/Reservations.svg?style=svg)](https://circleci.com/gh/gantonious/Reservations)

A lightweight backend to hold reservation data for events.

## Running

Before running make sure you have both `docker` and `docker-compose` available on your system. You will also need to define `POSTGRES_PASSWORD` as an environment variable. This will be used to configure the postgres password on initial run and will be used by the web services to authenticate into the database.

To get everything up and running do:

```bash
docker-compose build
docker-compose up -d
```