using NUnit.Framework;
using AcceptanceTests.Common.Api;
using Microsoft.Extensions.Configuration;
using NotifyApi.Common.Configuration;

namespace NotifyApi.IntegrationTests
{
    [SetUpFixture]
    public class TestSetupFixture
    {
        private ServicesConfiguration ServicesConfiguration => new ConfigurationBuilder()
                                                            .AddJsonFile("appsettings.json")
                                                            .Build()
                                                            .GetSection("Services")
                                                            .Get<ServicesConfiguration>();


        [OneTimeSetUp]
        public void StartZap()
        {
            Zap.Start();
        }

        [OneTimeTearDown]
        public void ZapReport()
        {
            Zap.ReportAndShutDown("NotifyApi-Integration", ServicesConfiguration.NotifyApiUrl);
        }
    }
}
