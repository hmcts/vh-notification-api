rmdir /q /s Artifacts

SET exclude=\"[*]Notify.API.Extensions.*,[Notify.API]Notify.API.ConfigureServicesExtensions,[Notify.API]Notify.API.Startup,[Notify.API]Notify.API.Program,[*]Notify.API.Swagger.*,[NotifyApi.*Tests?]*,[*]NotifyApi.DAL.SeedData.*,[*]NotifyApi.DAL.Migrations.*,[*]NotifyApi.DAL.Mappings.*,[*]NotifyApi.Domain.Ddd.*,[*]NotifyApi.Domain.Validations.*,[NotifyApi.DAL]NotifyApi.DAL.NotifyApiDbContext,[NotifyApi.DAL]NotifyApi.DAL.DesignTimeHearingsContextFactory,[*]NotifyApi.Common.*,[*]Testing.Common.*,[*]NotifyApi.Services.*\"
dotnet test NotifyApi.UnitTests/NotifyApi.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat="\"opencover,cobertura,json,lcov\"" /p:CoverletOutput=../Artifacts/Coverage/ /p:MergeWith='../Artifacts/Coverage/coverage.json' /p:Exclude="${exclude}"
dotnet test NotifyApi.IntegrationTests/NotifyApi.IntegrationTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat="\"opencover,cobertura,json,lcov\"" /p:CoverletOutput=../Artifacts/Coverage/ /p:MergeWith='../Artifacts/Coverage/coverage.json' /p:Exclude="${exclude}"

reportgenerator -reports:Artifacts/Coverage/coverage.opencover.xml -targetDir:Artifacts/Coverage/Report -reporttypes:HtmlInline_AzurePipelines

"Artifacts/Coverage/Report/index.htm"