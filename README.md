# vh-notification-api


## HMCTS

[![Build Status](https://dev.azure.com/hmctsreform/VirtualHearings/_apis/build/status/Apps-CI/hmcts.vh-notification-api?repoName=hmcts%2Fvh-notification-api&branchName=master)](https://dev.azure.com/hmctsreform/VirtualHearings/_build/latest?definitionId=188&repoName=hmcts%2Fvh-notification-api&branchName=master)

[![NotificationApi.Client package in vh-packages feed in Azure Artifacts](https://feeds.dev.azure.com/hmctsreform/3f69a23d-fbc7-4541-afc7-4cccefcad773/_apis/public/Packaging/Feeds/vh-packages/Packages/903ad9ea-874b-4201-9841-66894e4f6cc1/Badge)](https://dev.azure.com/hmctsreform/VirtualHearings/_artifacts/feed/vh-packages/NuGet/NotificationApi.Client?preferRelease=true)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=vh-notification-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=vh-notification-api)

## SDS

[![Build Status](https://dev.azure.com/hmcts/Video%20Hearings/_apis/build/status/vh-notification-api/hmcts.vh-notification-api.sds.master-release?repoName=hmcts%2Fvh-notification-api&branchName=master)](https://dev.azure.com/hmcts/Video%20Hearings/_build/latest?definitionId=667&repoName=hmcts%2Fvh-notification-api&branchName=master)

[![NotificationApi.Client package in vh-packages feed in Azure Artifacts](https://feeds.dev.azure.com/hmcts/cf3711aa-2aed-4f62-81a8-2afaee0ce26d/_apis/public/Packaging/Feeds/vh-packages/Packages/78c7eb3c-06f9-4718-879a-7f75ceb5b6ac/Badge)](https://dev.azure.com/hmcts/Video%20Hearings/_artifacts/feed/vh-packages/NuGet/NotificationApi.Client?preferRelease=true)


[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=vh-notification-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=vh-notification-api)

## Setup nuget sources

Include the vh-packages source

`https://pkgs.dev.azure.com/hmctsreform/VirtualHearings/_packaging/vh-packages/nuget/v3/index.json`

## Restore Tools

Run the following in a terminal at the root of the repository

``` shell
dotnet tool restore
```

## Generate HTML Report

Under the unit test project directory

```bash
dotnet reportgenerator "-reports:./Coverage/coverage.opencover.xml" "-targetDir:./Artifacts/Coverage/Report" -reporttypes:Html -sourcedirs:./NotificationApi
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


To update latest version of stryker please use the following command

```bash
dotnet tool update --global dotnet-stryker
```

## Running tests with Docker

### Setup a local instance of Sonar

``` shell
docker run -d --name sonarqube -e SONAR_ES_BOOTSTRAP_CHECKS_DISABLE=true -p 9000:9000 sonarqube:latest
```

The unit and integration tests can be run inside a container. You will need a an access token to build the image locally

Open a terminal at the root of the repo and run the following in a terminal to build the test image:

``` shell
docker build . --file tests/Dockerfile -t notification-api-tests --build-arg PAT=<PAT TOKEN>
docker run --name notification-api-local --network=host -it --mount src="$(pwd)",target=/app,type=bind notification-api-tests:latest
```

### Running all tests in Docker

Open a terminal at the root level of the repository and run the following command

```console
docker-compose -f "docker-compose.tests.yml" up --build --abort-on-container-exit
```

> You may need to create a `.env` file to store the environment variables
