PROJECT_PATH=./Reservations

source ~/.profile

if [ -z "${POSTGRES_PASSWORD}" ]; then
  echo "====GENERATING DB PASSWORD===="
  DB_PASSWORD="$(pwgen 50 1)"
  echo "export POSTGRES_PASSWORD=${DB_PASSWORD}" >> ~/.profile
  source ~/.profile
fi

if [ -d "$PROJECT_PATH" ]; then
  echo "====PULLING LATEST===="
  cd $PROJECT_PATH
  git pull
else
  echo "====CLONING FROM SCRATCH===="
  git clone https://github.com/gantonious/Reservations.git
  cd $PROJECT_PATH
fi

echo "====BUILDING SERVICES===="
docker-compose build
echo "====LAUNCHING SERVICES===="
docker-compose up -d