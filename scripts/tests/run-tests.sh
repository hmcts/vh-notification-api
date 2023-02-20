#!/bin/sh

set -x

dotnet sonarscanner begin /k:"${SONAR_PROJECT_KEY}" /o:"${SONAR_ORG}" /version:"${SONAR_PROJECT_VERSION}" /name:"${SONAR_PROJECT_NAME}" /d:sonar.host.url="${SONAR_HOST}" /d:sonar.login="${SONAR_TOKEN}" /d:sonar.cs.opencover.reportsPaths="${PWD}/Coverage/coverage.opencover.xml" /d:sonar.coverage.exclusions="**/NotificationApi/Swagger/**/*,**/Program.cs,**/Startup.cs,**/Testing.Common/**/*,**/NotificationApi.Common/**/*,**/NotificationApi.IntegrationTests/**/*,**/NotificationApi.UnitTests/**/*,**/NotificationApi/Extensions/*,**/NotificationApi.DAL/Migrations/**/*" /d:sonar.cpd.exclusions="**/Program.cs,**/NotificationType.cs,**/Startup.cs,**/Testing.Common/**/*,**/NotificationApi/Swagger/**/*,NotificationApi/NotificationApi.DAL/Migrations/*,NotificationApi/NotificationApi.DAL/TemplateDataForEnvironments.cs"

dotnet build NotificationApi/NotificationApi.sln -c Release

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

dotnet sonarscanner end /d:sonar.login="${SONAR_TOKEN}"
