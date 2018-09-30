PROJECT_PATH=./Reservations

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