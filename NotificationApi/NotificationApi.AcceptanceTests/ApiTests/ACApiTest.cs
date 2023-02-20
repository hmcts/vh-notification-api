using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotificationApi.Client;
using NotificationApi.Common.Configuration;
using NotificationApi.Common.Security;
using NUnit.Framework;
using Testing.Common.Configuration;

namespace NotificationApi.AcceptanceTests.ApiTests;

public abstract class ACApiTest
{
    private IConfigurationRoot _configRoot;
    private AzureAdConfiguration _azureConfiguration;
    private ServicesConfiguration _serviceConfiguration;
    protected NotificationApiClient NotificationApiClient;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _configRoot = ConfigRootBuilder.Build();
        RegisterSettings();
        var apiToken = GenerateApiToken();
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", apiToken);
        NotificationApiClient = NotificationApiClient.GetClient(_serviceConfiguration.NotificationApiUrl, httpClient);
    }
    
    private string GenerateApiToken()
    {
        return new AzureTokenProvider(Options.Create(_azureConfiguration)).GetClientAccessToken(_azureConfiguration.ClientId,
            _azureConfiguration.ClientSecret, _serviceConfiguration.VhNotificationApiResourceId);
    }

    public void RegisterSettings()
    {
        _azureConfiguration = _configRoot.GetSection("AzureAd").Get<AzureAdConfiguration>();
        _serviceConfiguration = _configRoot.GetSection("Services").Get<ServicesConfiguration>();
    }
}
