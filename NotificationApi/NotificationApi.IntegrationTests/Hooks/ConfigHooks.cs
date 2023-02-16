using System.Net.Http;
using AcceptanceTests.Common.Api;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotificationApi.Common.Configuration;
using NotificationApi.Common.Security;
using NotificationApi.DAL;
using NotificationApi.IntegrationTests.Contexts;
using NotificationApi.IntegrationTests.Helper;
using TechTalk.SpecFlow;
using Testing.Common.Configuration;
using Testing.Common.Security;
using VHConfigurationManager = AcceptanceTests.Common.Configuration.ConfigurationManager;

namespace NotificationApi.IntegrationTests.Hooks
{
    [Binding]
    public class ConfigHooks
    {
        private readonly IConfigurationRoot _configRoot;

        public ConfigHooks(IntTestContext context)
        {
            _configRoot = ConfigRootBuilder.Build();
            context.Config = new Config();
            context.Tokens = new NotificationApiTokens();
        }

        [BeforeScenario(Order = (int)HooksSequence.ConfigHooks)]
        public void RegisterSecrets(IntTestContext context)
        {
            var azureOptions = RegisterAzureSecrets(context);
            var notifyOptions = RegisterNotifySecrets(context);
            RegisterDefaultData();
            RegisterHearingServices(context);
            RegisterDatabaseSettings(context);
            RegisterServer(context);
            RegisterApiSettings(context);
            GenerateBearerTokens(context, azureOptions, notifyOptions);
        }

        private IOptions<AzureAdConfiguration> RegisterAzureSecrets(IntTestContext context)
        {
            var azureOptions = Options.Create(_configRoot.GetSection("AzureAd").Get<AzureAdConfiguration>());
            context.Config.AzureAdConfiguration = azureOptions.Value;
            VHConfigurationManager.VerifyConfigValuesSet(context.Config.AzureAdConfiguration);
            return azureOptions;
        }
        
        private NotifyConfiguration RegisterNotifySecrets(IntTestContext context)
        {
            var notifyOptions =  Options.Create(_configRoot.GetSection("NotifyConfiguration").Get<NotifyConfiguration>()).Value;
            context.Config.NotifyConfiguration = notifyOptions;
            VHConfigurationManager.VerifyConfigValuesSet(context.Config.NotifyConfiguration);
            return notifyOptions;
        }

        private static void RegisterDefaultData()
        {
            // Method intentionally left empty.
        }

        private void RegisterHearingServices(IntTestContext context)
        {
            context.Config.ServicesConfig = Options.Create(_configRoot.GetSection("Services").Get<ServicesConfiguration>()).Value;
            VHConfigurationManager.VerifyConfigValuesSet(context.Config.ServicesConfig);
        }

        private void RegisterDatabaseSettings(IntTestContext context)
        {
            context.Config.DbConnection = Options.Create(_configRoot.GetSection("ConnectionStrings").Get<ConnectionStringsConfig>()).Value;
            VHConfigurationManager.VerifyConfigValuesSet(context.Config.DbConnection);
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<NotificationsApiDbContext>();
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            dbContextOptionsBuilder.UseSqlServer(context.Config.DbConnection.VhNotificationsApi);
            context.NotifyBookingsDbContextOptions = dbContextOptionsBuilder.Options;
            context.TestDataManager = new TestDataManager(context.NotifyBookingsDbContextOptions);
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

        private static void GenerateBearerTokens(IntTestContext context, IOptions<AzureAdConfiguration> azureOptions,
            NotifyConfiguration notifyConfiguration)
        {
            context.Tokens.NotificationApiBearerToken = new AzureTokenProvider(azureOptions).GetClientAccessToken(
                azureOptions.Value.ClientId, azureOptions.Value.ClientSecret,
                context.Config.ServicesConfig.VhNotificationApiResourceId);
            context.Tokens.NotificationApiBearerToken.Should().NotBeNullOrEmpty();

            context.Tokens.NotificationCallbackBearerToken =
                new CustomJwtTokenProvider().GenerateTokenForCallbackEndpoint(notifyConfiguration.CallbackSecret, 60);
            context.Tokens.NotificationCallbackBearerToken.Should().NotBeNullOrWhiteSpace();
            
            Zap.SetAuthToken(context.Tokens.NotificationApiBearerToken);
        }
    }
}

