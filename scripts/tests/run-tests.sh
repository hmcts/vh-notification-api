#!/bin/sh

# echo "Current DIR ${PWD}"
# echo "ProjectKey ${SONAR_PROJECT_KEY}"
# echo "SONAR TOKEN ${SONAR_TOKEN}"
# echo "Sonar Host ${SONAR_HOST}"
# echo "Sonar Org ${SONAR_ORG}"
# echo "Sonar SONAR_PROJECT_VERSION ${SONAR_PROJECT_VERSION}"
# echo "Sonar SONAR_PROJECT_NAME ${SONAR_PROJECT_NAME}"

set -x

dotnet sonarscanner begin /k:"${SONAR_PROJECT_KEY}" /o:"${SONAR_ORG}" /version:"${SONAR_PROJECT_VERSION}" /name:"${SONAR_PROJECT_NAME}" /d:sonar.host.url="${SONAR_HOST}" /d:sonar.login="${SONAR_TOKEN}" /d:sonar.cs.opencover.reportsPaths="${PWD}/Coverage/coverage.opencover.xml"
echo "Building solution"

dotnet build NotificationApi/NotificationApi.sln -c Release

echo "Running tests"

# Script is for docker compose tests where the script is at the root level
dotnet test NotificationApi/NotificationApi.UnitTests/NotificationApi.UnitTests.csproj -c Release --no-build --results-directory ./TestResults --logger "trx;LogFileName=NotificationApi-Unit-Tests-TestResults.trx" \
    "/p:CollectCoverage=true" \
    "/p:Exclude=\"[*]Testing.Common.*,[*]NotificationApi.Common.*,[*.Common]*\"" \
    "/p:CoverletOutput=${PWD}/Coverage/" \
    "/p:MergeWith=/Coverage/coverage.json" \
    "/p:CoverletOutputFormat=\"opencover,json,cobertura,lcov\""

dotnet test NotificationApi/NotificationApi.IntegrationTests/NotificationApi.IntegrationTests.csproj -c Release --no-build --filter FullyQualifiedName~NotificationApi.IntegrationTests.Database --results-directory ./TestResults --logger "trx;LogFileName=NotificationApi-Integration-Tests-TestResults.trx" \
    "/p:CollectCoverage=true" \
    "/p:Exclude=\"[*]Testing.Common.*,[*]NotificationApi.Common.*,[*.Common]*\"" \
    "/p:CoverletOutput=${PWD}/Coverage/" \
    "/p:MergeWith=/Coverage/coverage.json" \
    "/p:CoverletOutputFormat=\"opencover,json,cobertura,lcov\""

echo "Sonar End"
dotnet sonarscanner end /d:sonar.login="${SONAR_TOKEN}"
