using System.Net.Http;
using AcceptanceTests.Common.Api;
using AcceptanceTests.Common.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotificationApi;
using NotificationApi.Common.Configuration;
using NotificationApi.Common.Security;
using NotificationApi.DAL;
using NotificationApi.IntegrationTests.Contexts;
using NotificationApi.IntegrationTests.Helper;
using TechTalk.SpecFlow;
using Testing.Common.Configuration;

namespace NotificationApi.IntegrationTests.Hooks
{
    [Binding]
    public class ConfigHooks
    {
        private readonly IConfigurationRoot _configRoot;

        public ConfigHooks(IntTestContext context)
        {
            _configRoot = ConfigurationManager.BuildConfig("4E35D845-27E7-4A19-BE78-CDA896BF907D");
            context.Config = new Config();
            context.Tokens = new NotificationApiTokens();
        }

        [BeforeScenario(Order = (int)HooksSequence.ConfigHooks)]
        public void RegisterSecrets(IntTestContext context)
        {
            var azureOptions = RegisterAzureSecrets(context);
            RegisterDefaultData(context);
            RegisterHearingServices(context);
            RegisterDatabaseSettings(context);
            RegisterServer(context);
            RegisterApiSettings(context);
            GenerateBearerTokens(context, azureOptions);
        }

        private IOptions<AzureAdConfiguration> RegisterAzureSecrets(IntTestContext context)
        {
            var azureOptions = Options.Create(_configRoot.GetSection("AzureAd").Get<AzureAdConfiguration>());
            context.Config.AzureAdConfiguration = azureOptions.Value;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.AzureAdConfiguration);
            return azureOptions;
        }

        private static void RegisterDefaultData(IntTestContext context)
        {
            // Method intentionally left empty.
        }

        private void RegisterHearingServices(IntTestContext context)
        {
            context.Config.ServicesConfig = Options.Create(_configRoot.GetSection("Services").Get<ServicesConfiguration>()).Value;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.ServicesConfig);
        }

        private void RegisterDatabaseSettings(IntTestContext context)
        {
            context.Config.DbConnection = Options.Create(_configRoot.GetSection("ConnectionStrings").Get<ConnectionStringsConfig>()).Value;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.DbConnection);
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<NotificationsApiDbContext>();
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            dbContextOptionsBuilder.UseSqlServer(context.Config.DbConnection.VhNotificationsApi);
            context.NotifyBookingsDbContextOptions = dbContextOptionsBuilder.Options;
            context.TestDataManager = new TestDataManager(context.Config.ServicesConfig, context.NotifyBookingsDbContextOptions);
        }

        private static void RegisterServer(IntTestContext context)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder()
                    .UseKestrel(c => c.AddServerHeader = false)
                    .UseEnvironment("Development")
                    .UseStartup<Startup>();
            context.Server = new TestServer(webHostBuilder);
        }

        private static void RegisterApiSettings(IntTestContext context)
        {
            context.Response = new HttpResponseMessage(); 
        }

        private static void GenerateBearerTokens(IntTestContext context, IOptions<AzureAdConfiguration> azureOptions)
        {
            context.Tokens.NotificationApiBearerToken = new AzureTokenProvider(azureOptions).GetClientAccessToken(
                azureOptions.Value.ClientId, azureOptions.Value.ClientSecret,
                context.Config.ServicesConfig.VhNotificationApiResourceId);
            context.Tokens.NotificationApiBearerToken.Should().NotBeNullOrEmpty();

            Zap.SetAuthToken(context.Tokens.NotificationApiBearerToken);
        }
    }
}

