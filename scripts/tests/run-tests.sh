#!/bin/sh
set -x

rm -d -r ${PWD}/Coverage
rm -d -r ${PWD}/TestResults

dotnet sonarscanner begin /k:"${SONAR_PROJECT_KEY}" /o:"${SONAR_ORG}" /version:"${SONAR_PROJECT_VERSION}" /name:"${SONAR_PROJECT_NAME}" /d:sonar.host.url="${SONAR_HOST}" /d:sonar.login="${SONAR_TOKEN}" /s:"${PWD}/vh-api-sonar-settings.xml"

exclusions="[Testing.Common]*,[NotificationApi.Common]NotificationApi.Common.*,[NotificationApi.Domain]*.Ddd*,[NotificationApi.DAL]*.Migrations*"
configuration=Release

dotnet build NotificationApi/NotificationApi.sln -c $configuration
# Script is for docker compose tests where the script is at the root level
dotnet test NotificationApi/NotificationApi.UnitTests/NotificationApi.UnitTests.csproj -c $configuration --no-build --results-directory ./TestResults --logger "trx;LogFileName=NotificationApi-Unit-Tests-TestResults.trx" \
    "/p:CollectCoverage=true" \
    "/p:Exclude=\"${exclusions}\"" \
    "/p:CoverletOutput=${PWD}/Coverage/" \
    "/p:MergeWith=${PWD}/Coverage/coverage.json" \
    "/p:CoverletOutputFormat=\"opencover,json,cobertura,lcov\""

dotnet test NotificationApi/NotificationApi.IntegrationTests/NotificationApi.IntegrationTests.csproj -c $configuration --no-build --results-directory ./TestResults --logger "trx;LogFileName=NotificationApi-Integration-Tests-TestResults.trx" \
    "/p:CollectCoverage=true" \
    "/p:Exclude=\"${exclusions}\"" \
    "/p:CoverletOutput=${PWD}/Coverage/" \
    "/p:MergeWith=${PWD}/Coverage/coverage.json" \
    "/p:CoverletOutputFormat=\"opencover,json,cobertura,lcov\""

dotnet sonarscanner end /d:sonar.login="${SONAR_TOKEN}"
