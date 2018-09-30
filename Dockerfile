FROM microsoft/dotnet:latest
EXPOSE 5000/tcp
COPY . ./app
WORKDIR ./app
RUN dotnet restore
ENTRYPOINT /bin/sh ./start.sh