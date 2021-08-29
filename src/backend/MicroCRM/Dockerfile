FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env

COPY ./src/backend/MicroCRM.Services.Encryption /backend/MicroCRM.Services.Encryption
COPY ./src/backend/MicroCRM.Services.Random /backend/MicroCRM.Services.Random
COPY ./src/backend/MicroCRM /backend/MicroCRM

WORKDIR /backend/MicroCRM

RUN dotnet restore
RUN dotnet build --configuration Release --no-restore
RUN dotnet publish --configuration Release --no-restore --output publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY --from=build-env /backend/MicroCRM/publish .

ENTRYPOINT [ "dotnet", "MicroCRM.dll" ]
