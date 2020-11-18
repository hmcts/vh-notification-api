using NUnit.Framework;
using AcceptanceTests.Common.Api;
using Microsoft.Extensions.Configuration;
using NotificationApi.Common.Configuration;

namespace NotificationApi.IntegrationTests
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
            Zap.ReportAndShutDown("NotifyApi-Integration", ServicesConfiguration.NotificationApiUrl);
        }
    }
}
