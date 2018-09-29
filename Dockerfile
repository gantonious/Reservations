FROM microsoft/dotnet:latest
EXPOSE 5000/tcp
COPY . ./app
WORKDIR ./app
RUN ["dotnet", "restore"]
ENTRYPOINT ["dotnet", "run", "--project", "Reservations.WebServices/Reservations.WebServices.csproj"]