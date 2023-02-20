using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotificationApi.Common.Configuration;
using NotificationApi.DAL;
using NotificationApi.IntegrationTests.Helper;
using NUnit.Framework;
using Testing.Common.Configuration;
using Testing.Common.Security;

namespace NotificationApi.IntegrationTests.Api.Setup
{
    public class ApiTest
    {
        private const string CallbackSecretConfigKey = "NotifyConfiguration:CallbackSecret";
        protected WebApplicationFactory<Program> Application = null!;
        protected TestDataManager TestDataManager = null!;
        protected DbContextOptions<NotificationsApiDbContext> DbOptions { get; private set; }
        private IConfigurationRoot _configRoot;
        private NotifyConfiguration _notifyConfiguration;
        private string _databaseConnectionString;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            GenerateRandomCallbackSecret();
            RegisterSettings();
        
            Application = new VhApiWebApplicationFactory();
            InitTestDataManager();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Environment.SetEnvironmentVariable(CallbackSecretConfigKey, null);
        }

        private void InitTestDataManager()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<NotificationsApiDbContext>();
            _databaseConnectionString = _configRoot.GetConnectionString("VhNotificationsApi");
            dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString);
            DbOptions = dbContextOptionsBuilder.Options;
            TestDataManager = new TestDataManager(dbContextOptionsBuilder.Options);
        }

        /// <summary>
        /// This will insert a random callback secret per test run
        /// </summary>
        private static void GenerateRandomCallbackSecret()
        {
            var secret = Convert.ToBase64String(new HMACSHA256().Key);
            Environment.SetEnvironmentVariable(CallbackSecretConfigKey, secret);
        }

        protected string GenerateCallbackToken()
        {
            return new CustomJwtTokenProvider().GenerateTokenForCallbackEndpoint(_notifyConfiguration.CallbackSecret, 60);
        }

        private void RegisterSettings()
        {
            _configRoot = ConfigRootBuilder.Build();
            _notifyConfiguration = _configRoot.GetSection("NotifyConfiguration").Get<NotifyConfiguration>();
        }
    }
}
