# vh-notification-api

[![Build Status](https://dev.azure.com/hmctsreform/VirtualHearings/_apis/build/status/Apps-CI/hmcts.vh-notification-api?repoName=hmcts%2Fvh-notification-api&branchName=VIH-6688_EndPoints)](https://dev.azure.com/hmctsreform/VirtualHearings/_build/latest?definitionId=188&repoName=hmcts%2Fvh-notification-api&branchName=VIH-6688_EndPoints)

[![NotificationApi.Client package in vh-packages feed in Azure Artifacts](https://feeds.dev.azure.com/hmctsreform/3f69a23d-fbc7-4541-afc7-4cccefcad773/_apis/public/Packaging/Feeds/e48b2732-376c-4052-ba97-b28783c9bab5/Packages/903ad9ea-874b-4201-9841-66894e4f6cc1/Badge)](https://dev.azure.com/hmctsreform/VirtualHearings/_packaging?_a=package&feed=e48b2732-376c-4052-ba97-b28783c9bab5&package=903ad9ea-874b-4201-9841-66894e4f6cc1&preferRelease=true)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=vh-notification-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=vh-notification-api)

## Setup nuget sources

Include the vh-packages source

```
https://pkgs.dev.azure.com/hmctsreform/VirtualHearings/_packaging/vh-packages/nuget/v3/index.json
```

Include the govuk notify source

```
https://api.bintray.com/nuget/gov-uk-notify/nuget
```

## Setup templates locally

Execute the environment template scripts to setup your local against it's respective notify environment

## Running code coverage

First ensure you are running a terminal in the Notification Api directory of this repository and then run the following commands.

```bash
dotnet test --no-build NotificationApi.UnitTests/NotificationApi.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat="\"opencover,cobertura,json,lcov\"" /p:CoverletOutput=../Artifacts/Coverage/ /p:MergeWith='../Artifacts/Coverage/coverage.json' /p:Exclude="\"[*]NotificationApi.API.Extensions.*,[NotificationApi]NotificationApi.Startup,[NotificationApi]NotificationApi.Program,[*]NotificationApi.Swagger.*,[NotificationApi.*Tests?]*,[*]NotificationApi.DAL.Migrations.*,[*]NotificationApi.DAL.Mappings.*,[*]NotificationApi.Domain.Ddd.*,[*]NotificationApi.Domain.Validations.*,[NotificationApi.DAL]NotificationApi.DAL.NotificationApiDbContext,[NotificationApi.DAL]NotificationApi.DAL.DesignTimeHearingsContextFactory,[*]NotificationApi.Common.*,[*]Testing.Common.*"

dotnet test --no-build NotificationApi.IntegrationTests/NotificationApi.IntegrationTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat="\"opencover,cobertura,json,lcov\"" /p:CoverletOutput=../Artifacts/Coverage/ /p:MergeWith='../Artifacts/Coverage/coverage.json' /p:Exclude="\"[*]NotificationApi.API.Extensions.*,[NotificationApi]NotificationApi.Startup,[NotificationApi]NotificationApi.Program,[*]NotificationApi.Swagger.*,[NotificationApi.*Tests?]*,[*]NotificationApi.DAL.Migrations.*,[*]NotificationApi.DAL.Mappings.*,[*]NotificationApi.Domain.Ddd.*,[*]NotificationApi.Domain.Validations.*,[NotificationApi.DAL]NotificationApi.DAL.NotificationApiDbContext,[NotificationApi.DAL]NotificationApi.DAL.DesignTimeHearingsContextFactory,[*]NotificationApi.Common.*,[*]Testing.Common.*"

```

## Generate HTML Report

Under the unit test project directory

```bash
dotnet reportgenerator "-reports:../Artifacts/Coverage/coverage.opencover.xml" "-targetDir:../Artifacts/Coverage/Report" -reporttypes:HtmlInline_AzurePipelines
```

## Branch name

git hook will run on pre commit and control the standard for new branch name.

The branch name should start with: feature/VIH-XXXX-branchName (X - is digit).
If git version is less than 2.9 the pre-commit file from the .githooks folder need copy to local .git/hooks folder.
To change git hooks directory to directory under source control run (works only for git version 2.9 or greater) :
$ git config core.hooksPath .githooks

## Commit message

The commit message will be validated by prepare-commit-msg hook.
The commit message format should start with : 'feature/VIH-XXXX : ' folowing by 8 or more characters description of commit, otherwise the warning message will be presented.

## Run Zap scan locally

To run Zap scan locally update the following settings and run acceptance\integration tests

User Secrets:

- "Services:NotificationApiUrl": "https://NotificationApi_AC/"

Update following configuration under appsettings.json under NotificationApi.AcceptanceTests or NotificationApi.IntegrationTests

- "Services:NotificationApiUrl": "https://NotificationApi_AC/"
- "ZapConfiguration:ZapScan": true
- "ConnectionStrings:VhNotificationApi": "Server=localhost,1433;Database=VhNotificationApi;User=sa;Password=VeryStrongPassword!;" (IntegrationTest alone)

Note: Ensure you have Docker desktop engine installed and setup

## Run Stryker

To run stryker mutation test, go to UnitTest folder under command prompt and run the following command

```bash
dotnet stryker
```

From the results look for line(s) of code highlighted with Survived\No Coverage and fix them.

If in case you have not installed stryker previously, please use one of the following commands

### Global

```bash
dotnet tool install -g dotnet-stryker
```

### Local

```bash
dotnet tool install dotnet-stryker
```

To update latest version of stryker please use the following command

```bash
dotnet tool update --global dotnet-stryker
```

## Running tests with Docker

The unit and integration tests can be run inside a container. You will need a an access token to build the image locally

Open a terminal at the root of the repo and run the following in a terminal to build the test image:

``` shell
docker build . --file tests/Dockerfile -t notification-api-tests --build-arg PAT=<PAT TOKEN>
```

### Running all tests in Docker

Open a terminal at the root level of the repository and run the following command

```console
docker-compose -f "docker-compose.tests.yml" up --build --abort-on-container-exit
```

> You may need to create a `.env` file to store the environment variables
