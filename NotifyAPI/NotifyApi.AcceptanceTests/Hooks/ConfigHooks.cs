using System.Threading.Tasks;
using AcceptanceTests.Common.Api;
using AcceptanceTests.Common.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TechTalk.SpecFlow;
using Testing.Common.Configuration;
using NotifyApi.AcceptanceTests.Contexts;
using NotifyApi.Common.Configuration;

namespace NotifyApi.AcceptanceTests.Hooks
{
    [Binding]
    public class ConfigHooks
    {
        private readonly IConfigurationRoot _configRoot;

        public ConfigHooks(TestContext context)
        {
            _configRoot = ConfigurationManager.BuildConfig("9AECE566-336D-4D16-88FA-7A76C27321CD", GetTargetEnvironment());
            context.Config = new Config();
            context.Tokens = new NotifyApiTokens();
        }

        private static string GetTargetEnvironment()
        {
            return NUnit.Framework.TestContext.Parameters["TargetEnvironment"] ?? "";
        }

        [BeforeScenario(Order = (int)HooksSequence.ConfigHooks)]
        public async Task RegisterSecrets(TestContext context)
        {
            RegisterAzureSecrets(context);
            RegisterHearingServices(context);
            await GenerateBearerTokens(context);
        }

        private void RegisterAzureSecrets(TestContext context)
        {
            context.Config.AzureAdConfiguration = Options.Create(_configRoot.GetSection("AzureAd").Get<AzureAdConfiguration>()).Value;
            context.Config.AzureAdConfiguration.Authority += context.Config.AzureAdConfiguration.TenantId;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.AzureAdConfiguration);
        }

        private void RegisterHearingServices(TestContext context)
        {
            context.Config.VhServices = Options.Create(_configRoot.GetSection("Services").Get<ServicesConfiguration>()).Value;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.VhServices);
        }

        private static async Task GenerateBearerTokens(TestContext context)
        {
            var azureConfig = new AzureAdConfig()
            {
                Authority = context.Config.AzureAdConfiguration.Authority,
                ClientId = context.Config.AzureAdConfiguration.ClientId,
                ClientSecret = context.Config.AzureAdConfiguration.ClientSecret,
                TenantId = context.Config.AzureAdConfiguration.TenantId
            };

            context.Tokens.NotifyApiBearerToken = await ConfigurationManager.GetBearerToken(
                azureConfig, context.Config.VhServices.VhNotifyApiResourceId);
            context.Tokens.NotifyApiBearerToken.Should().NotBeNullOrEmpty();
            
            Zap.SetAuthToken(context.Tokens.NotifyApiBearerToken);
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
