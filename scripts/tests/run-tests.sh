#!/bin/sh
set -x

exclusions="[Testing.Common]*,[NotificationApi.Common]NotificationApi.Common.*,[NotificationApi.Domain]*.Ddd*,[NotificationApi.DAL]*.Migrations*"
configuration=Release

# Script is for docker compose tests where the script is at the root level
dotnet test NotificationApi/NotificationApi.UnitTests/NotificationApi.UnitTests.csproj -c $configuration --results-directory ./TestResults --logger "trx;LogFileName=NotificationApi-Unit-Tests-TestResults.trx" \
    "/p:CollectCoverage=true" \
    "/p:Exclude=\"${exclusions}\"" \
    "/p:CoverletOutput=${PWD}/Coverage/" \
    "/p:MergeWith=${PWD}/Coverage/coverage.json" \
    "/p:CoverletOutputFormat=\"opencover,json,cobertura,lcov\""

dotnet test NotificationApi/NotificationApi.IntegrationTests/NotificationApi.IntegrationTests.csproj -c $configuration --results-directory ./TestResults --logger "trx;LogFileName=NotificationApi-Integration-Tests-TestResults.trx" \
    "/p:CollectCoverage=true" \
    "/p:Exclude=\"${exclusions}\"" \
    "/p:CoverletOutput=${PWD}/Coverage/" \
    "/p:MergeWith=${PWD}/Coverage/coverage.json" \
    "/p:CoverletOutputFormat=\"opencover,json,cobertura,lcov\""
