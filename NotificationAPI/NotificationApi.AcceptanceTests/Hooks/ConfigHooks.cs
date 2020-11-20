using System.Threading.Tasks;
using AcceptanceTests.Common.Api;
using AcceptanceTests.Common.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Common.Configuration;
using TechTalk.SpecFlow;
using Testing.Common.Configuration;

namespace NotificationApi.AcceptanceTests.Hooks
{
    [Binding]
    public class ConfigHooks
    {
        private readonly IConfigurationRoot _configRoot;

        public ConfigHooks(AcTestContext context)
        {
            _configRoot = ConfigurationManager.BuildConfig("4E35D845-27E7-4A19-BE78-CDA896BF907D", GetTargetEnvironment());
            context.Config = new Config();
            context.Tokens = new NotificationApiTokens();
        }

        private static string GetTargetEnvironment()
        {
            return NUnit.Framework.TestContext.Parameters["TargetEnvironment"] ?? "";
        }

        [BeforeScenario(Order = (int)HooksSequence.ConfigHooks)]
        public async Task RegisterSecrets(AcTestContext context)
        {
            RegisterAzureSecrets(context);
            RegisterHearingServices(context);
            await GenerateBearerTokens(context);
        }

        private void RegisterAzureSecrets(AcTestContext context)
        {
            context.Config.AzureAdConfiguration = Options.Create(_configRoot.GetSection("AzureAd").Get<AzureAdConfiguration>()).Value;
            context.Config.AzureAdConfiguration.Authority += context.Config.AzureAdConfiguration.TenantId;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.AzureAdConfiguration);
        }

        private void RegisterHearingServices(AcTestContext context)
        {
            context.Config.ServicesConfig = Options.Create(_configRoot.GetSection("Services").Get<ServicesConfiguration>()).Value;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.ServicesConfig);
        }

        private static async Task GenerateBearerTokens(AcTestContext context)
        {
            var azureConfig = new AzureAdConfig()
            {
                Authority = context.Config.AzureAdConfiguration.Authority,
                ClientId = context.Config.AzureAdConfiguration.ClientId,
                ClientSecret = context.Config.AzureAdConfiguration.ClientSecret,
                TenantId = context.Config.AzureAdConfiguration.TenantId
            };

            context.Tokens.NotificationApiBearerToken = await ConfigurationManager.GetBearerToken(
                azureConfig, context.Config.ServicesConfig.VhNotificationApiResourceId);
            context.Tokens.NotificationApiBearerToken.Should().NotBeNullOrEmpty();
            
            Zap.SetAuthToken(context.Tokens.NotificationApiBearerToken);
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
