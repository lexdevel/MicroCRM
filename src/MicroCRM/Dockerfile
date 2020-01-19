FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env

ARG BUILD_TYPE

COPY ./src/MicroCRM.Services.Encryption ./src/MicroCRM.Services.Encryption
COPY ./src/MicroCRM.Services.Random ./src/MicroCRM.Services.Random
COPY ./src/MicroCRM ./src/MicroCRM

WORKDIR /src/MicroCRM
RUN dotnet restore
RUN dotnet build -c ${BUILD_TYPE} --no-restore
RUN dotnet publish -c ${BUILD_TYPE} --no-restore -o publish

FROM node:latest as angular-env

COPY ./src/MicroCRM ./src/MicroCRM
WORKDIR /src/MicroCRM

RUN npm install
RUN npm run-script build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY --from=build-env /src/MicroCRM/publish .
COPY --from=angular-env /src/MicroCRM/WebApp/dist ./wwwroot

ENTRYPOINT [ "dotnet", "MicroCRM.dll" ]
