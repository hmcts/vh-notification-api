using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NotificationApi.Common.Configuration;
using NotificationApi.Common.Security;
using NotificationApi.DAL;
using NotificationApi.IntegrationTests.Helper;
using NUnit.Framework;
using Testing.Common.Configuration;
using Testing.Common.Security;

namespace NotificationApi.IntegrationTests.Api.Setup;

public class ApiTest
{
    private readonly string CallbackSecretConfigKey = "NotifyConfiguration:CallbackSecret";
    protected WebApplicationFactory<Program> Application = null!;
    protected TestDataManager TestDataManager = null!;
    protected DbContextOptions<NotificationsApiDbContext> DbOptions { get; private set; }
    private IConfigurationRoot _configRoot;
    private AzureAdConfiguration _azureConfiguration;
    private ServicesConfiguration _serviceConfiguration;
    private NotifyConfiguration _notifyConfiguration;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        GenerateRandomCallbackSecret();
        RegisterSettings();
        var apiToken = "delete me";//GenerateApiToken();
        
        Application = new VhApiWebApplicationFactory(apiToken);
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
        dbContextOptionsBuilder.UseInMemoryDatabase("InMemoryDbForTesting");
        DbOptions = dbContextOptionsBuilder.Options;
        TestDataManager = new TestDataManager(dbContextOptionsBuilder.Options);
    }

    private void GenerateRandomCallbackSecret()
    {
        var secret = Convert.ToBase64String(new HMACSHA256().Key);
        Environment.SetEnvironmentVariable(CallbackSecretConfigKey, secret);
    }

    // private string GenerateApiToken()
    // {
    //     return new AzureTokenProvider(Options.Create(_azureConfiguration)).GetClientAccessToken(
    //         _azureConfiguration.ClientId,
    //         _azureConfiguration.ClientSecret, _serviceConfiguration.VhNotificationApiResourceId);
    // }

    protected string GenerateCallbackToken()
    {
        return new CustomJwtTokenProvider().GenerateTokenForCallbackEndpoint(_notifyConfiguration.CallbackSecret, 60);
    }

    private void RegisterSettings()
    {
        var userSecretsId = "D76B6EB8-F1A2-4A51-9B8F-21E1B6B81E4F";
        _configRoot = ConfigRootBuilder.Build();

        _azureConfiguration = _configRoot.GetSection("AzureAd").Get<AzureAdConfiguration>();
        _serviceConfiguration = _configRoot.GetSection("Services").Get<ServicesConfiguration>();
        _notifyConfiguration = _configRoot.GetSection("NotifyConfiguration").Get<NotifyConfiguration>();
    }
}
