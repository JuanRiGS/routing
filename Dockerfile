#FROM microsoft/aspnetcore:2.0
#LABEL Name=netcore Version=0.0.1
#ARG source=.
#WORKDIR /app
#EXPOSE 9002
#COPY $source .
#COPY --from=build /output/bin/Debug/netcoreapp2.0/routing.xml /app/routing.xml
#ENTRYPOINT dotnet routing.dll

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /code
COPY . .
RUN dotnet restore
RUN dotnet publish --output /output --configuration Release
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build /output /app
EXPOSE 9002
ENTRYPOINT [ "dotnet", "routing.dll" ]
