using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AcceptanceTests.Common.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Client;
using NotificationApi.Common.Configuration;
using Notify.Client;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Testing.Common.Configuration;
using Testing.Common.Security;

namespace NotificationApi.AcceptanceTests.Hooks
{
    [Binding]
    public class ConfigHooks
    {
        private readonly IConfigurationRoot _configRoot;

        public ConfigHooks(AcTestContext context)
        {
            _configRoot =
                ConfigurationManager.BuildConfig("4E35D845-27E7-4A19-BE78-CDA896BF907D", GetTargetEnvironment());
            context.Config = new Config();
            context.Tokens = new NotificationApiTokens();
        }

        private static string GetTargetEnvironment()
        {
            return TestContext.Parameters["TargetEnvironment"] ?? "";
        }

        [BeforeScenario(Order = (int) HooksSequence.ConfigHooks)]
        public async Task RegisterSecrets(AcTestContext context)
        {
            await TestContext.Out.WriteLineAsync("Registering secrets");
            RegisterAzureSecrets(context);
            RegisterHearingServices(context);
            await GenerateBearerTokens(context);
            InitApiClient(context);
            InitApiCallbackClient(context);
            await TestContext.Out.WriteLineAsync("Registering secrets complete");
        }

        private void RegisterAzureSecrets(AcTestContext context)
        {
            TestContext.Out.WriteLine("Registering Azure secrets");
            context.Config.AzureAdConfiguration =
                Options.Create(_configRoot.GetSection("AzureAd").Get<AzureAdConfiguration>()).Value;
            context.Config.AzureAdConfiguration.Authority += context.Config.AzureAdConfiguration.TenantId;
            context.Config.AzureAdConfiguration.ClientId.Should().NotBeNullOrWhiteSpace();
            context.Config.AzureAdConfiguration.ClientSecret.Should().NotBeNullOrWhiteSpace();
            context.Config.AzureAdConfiguration.TenantId.Should().NotBeNullOrWhiteSpace();
            ConfigurationManager.VerifyConfigValuesSet(context.Config.AzureAdConfiguration);
            TestContext.Out.WriteLine("Registering Azure secrets complete");
        }

        private void RegisterHearingServices(AcTestContext context)
        {
            TestContext.Out.WriteLine("Registering hearing services");
            context.Config.ServicesConfig =
                Options.Create(_configRoot.GetSection("Services").Get<ServicesConfiguration>()).Value;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.ServicesConfig);
            
            context.Config.NotifyConfiguration =
                Options.Create(_configRoot.GetSection("NotifyConfiguration").Get<NotifyConfiguration>()).Value;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.NotifyConfiguration);
            TestContext.Out.WriteLine("Registering hearing services complete");
        }

        private static async Task GenerateBearerTokens(AcTestContext context)
        {
            await TestContext.Out.WriteLineAsync("Generating bearer tokens");
            var azureConfig = new AzureAdConfig()
            {
                Authority = context.Config.AzureAdConfiguration.Authority,
                ClientId = context.Config.AzureAdConfiguration.ClientId,
                ClientSecret = context.Config.AzureAdConfiguration.ClientSecret,
                TenantId = context.Config.AzureAdConfiguration.TenantId
            };

            context.Tokens.NotificationApiBearerToken = await ConfigurationManager.GetBearerToken(
                azureConfig, context.Config.ServicesConfig.VhNotificationApiResourceId);
            context.Tokens.NotificationApiBearerToken.Should().NotBeNullOrEmpty("Bearer token for api must be set");

            context.Tokens.NotificationCallbackBearerToken =
                new CustomJwtTokenProvider().GenerateTokenForCallbackEndpoint(
                    context.Config.NotifyConfiguration.CallbackSecret, 60);
            context.Tokens.NotificationCallbackBearerToken.Should()
                .NotBeNullOrWhiteSpace("Bearer token for callback must be set");
        }

        private static void InitApiClient(AcTestContext context)
        {
            TestContext.Out.WriteLine("Initialising API Client");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", context.Tokens.NotificationApiBearerToken);
            var baseUrl = context.Config.ServicesConfig.NotificationApiUrl;
            context.ApiClient = NotificationApiClient.GetClient(baseUrl, httpClient);
            context.NotifyClient = new NotificationClient(context.Config.NotifyConfiguration.ApiKey);
        }
        
        private static void InitApiCallbackClient(AcTestContext context)
        {
            TestContext.Out.WriteLine("Initialising API Callback Client");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", context.Tokens.NotificationCallbackBearerToken);
            var baseUrl = context.Config.ServicesConfig.NotificationApiUrl;
            context.ApiCallbackClient = NotificationApiClient.GetClient(baseUrl, httpClient);
        }
    }

    internal class AzureAdConfig : IAzureAdConfig
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
    }
}
