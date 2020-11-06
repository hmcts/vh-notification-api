# vh-notify-api

## Setup nuget sources
Include the vh-packages source

```
https://pkgs.dev.azure.com/hmctsreform/VirtualHearings/_packaging/vh-packages/nuget/v3/index.json
```

Include the govuk notify source

```
https://api.bintray.com/nuget/gov-uk-notify/nuget
```

## Running Sonar Analysis

``` bash
dotnet sonarscanner begin /k:"vh-notify-api" /d:sonar.cs.opencover.reportsPaths="NotifyAPI/Artifacts/Coverage/coverage.opencover.xml" /d:sonar.coverage.exclusions="Notify.API/Program.cs,Notify.API/Startup.cs,Notify.API/Extensions/**,Notify.API/Swagger/**,**/Notify.API/ConfigureServicesExtensions.cs,**/Testing.Common/**,**/Testing.Common/Helper/,Helper/Builders/Api/,Helper/Builders/Domain/,NotifyApi.Common/**,NotifyApi.DAL/Mappings/**,NotifyApi.DAL/SeedData/**,NotifyApi.DAL/NotifyApiDbContext.cs,NotifyApi.DAL/**/DesignTimeHearingsContextFactory.cs,NotifyApi.DAL/Migrations/**,NotifyApi.Domain/Ddd/**,NotifyApi.Domain/Validations/**" /d:sonar.cpd.exclusions="NotifyApi.DAL/Migrations/**" /d:sonar.verbose=true
dotnet build NotifyAPI/NotifyApi.sln
dotnet sonarscanner end
```

## Running code coverage

First ensure you are running a terminal in the NotifyAPI directory of this repository and then run the following commands.

``` bash
dotnet test --no-build NotifyApi.UnitTests/NotifyApi.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat="\"opencover,cobertura,json,lcov\"" /p:CoverletOutput=../Artifacts/Coverage/ /p:MergeWith='../Artifacts/Coverage/coverage.json' /p:Exclude="\"[*]Notify.API.Extensions.*,[Notify.API]Notify.API.ConfigureServicesExtensions,[Notify.API]Notify.API.Startup,[Notify.API]Notify.API.Program,[*]Notify.API.Swagger.*,[NotifyApi.*Tests?]*,[*]NotifyApi.DAL.SeedData.*,[*]NotifyApi.DAL.Migrations.*,[*]NotifyApi.DAL.Mappings.*,[*]NotifyApi.Domain.Ddd.*,[*]NotifyApi.Domain.Validations.*,[NotifyApi.DAL]NotifyApi.DAL.NotifyApiDbContext,[NotifyApi.DAL]NotifyApi.DAL.DesignTimeHearingsContextFactory,[*]NotifyApi.Common.*,[*]Testing.Common.*,[*]NotifyApi.Services.*\""

dotnet test --no-build NotifyApi.IntegrationTests/NotifyApi.IntegrationTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat="\"opencover,cobertura,json,lcov\"" /p:CoverletOutput=../Artifacts/Coverage/ /p:MergeWith='../Artifacts/Coverage/coverage.json' /p:Exclude="\"[*]Notify.API.Extensions.*,[Notify.API]Notify.API.ConfigureServicesExtensions,[Notify.API]Notify.API.Startup,[Notify.API]Notify.API.Program,[*]Notify.API.Swagger.*,[NotifyApi.*Tests?]*,[*]NotifyApi.DAL.SeedData.*,[*]NotifyApi.DAL.Migrations.*,[*]NotifyApi.DAL.Mappings.*,[*]NotifyApi.Domain.Ddd.*,[*]NotifyApi.Domain.Validations.*,[NotifyApi.DAL]NotifyApi.DAL.NotifyApiDbContext,[NotifyApi.DAL]NotifyApi.DAL.DesignTimeHearingsContextFactory,[*]NotifyApi.Common.*,[*]Testing.Common.*,[*]NotifyApi.Services.*\""

```

## Generate HTML Report

Under the unit test project directory

``` bash
dotnet reportgenerator "-reports:../Artifacts/Coverage/coverage.opencover.xml" "-targetDir:../Artifacts/Coverage/Report" -reporttypes:HtmlInline_AzurePipelines
```

## Branch name 
git hook will run on pre commit and control the standard for new branch name.

The branch name should start with: feature/VIH-XXXX-branchName  (X - is digit).
If git version is less than 2.9 the pre-commit file from the .githooks folder need copy to local .git/hooks folder.
To change git hooks directory to directory under source control run (works only for git version 2.9 or greater) :
$ git config core.hooksPath .githooks

## Commit message 
The commit message will be validated by prepare-commit-msg hook.
The commit message format should start with : 'feature/VIH-XXXX : ' folowing by 8 or more characters description of commit, otherwise the warning message will be presented.

## Run Zap scan locally

To run Zap scan locally update the following settings and run acceptance\integration tests

User Secrets:

- "Services:NotifyApiUrl": "https://NotifyApi_AC/"

Update following configuration under appsettings.json under NotifyApi.AcceptanceTests or  NotifyApi.IntegrationTests

- "Services:NotifyApiUrl": "https://NotifyApi_AC/"
- "ZapConfiguration:ZapScan": true
- "ConnectionStrings:VhNotifyApi": "Server=localhost,1433;Database=VhNotifyApi;User=sa;Password=VeryStrongPassword!;" (IntegrationTest alone)

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