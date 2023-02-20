using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotificationApi.Client;
using NotificationApi.Common.Configuration;
using NotificationApi.Common.Security;
using Notify.Client;
using NUnit.Framework;
using Testing.Common.Configuration;
using Testing.Common.Security;

namespace NotificationApi.AcceptanceTests.ApiTests;

public abstract class AcApiTest
{
    private IConfigurationRoot _configRoot;
    private AzureAdConfiguration _azureConfiguration;
    private ServicesConfiguration _serviceConfiguration;
    private NotifyConfiguration _notifyConfiguration;
    protected NotificationApiClient NotificationApiClient;
    protected NotificationApiClient NotificationApiCallbackClient;
    protected NotificationClient NotifyClient { get; set; }
    

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        RegisterSettings();
        InitApiClients();
    }

    private void InitApiClients()
    {
        var apiToken = GenerateApiToken();
        var notificationApiHttpClient = new HttpClient();
        notificationApiHttpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", apiToken);
        NotificationApiClient = NotificationApiClient.GetClient(_serviceConfiguration.NotificationApiUrl, notificationApiHttpClient);
        NotifyClient = new NotificationClient(_notifyConfiguration.ApiKey);
        
        var callbackToken = GenerateCallbackToken();
        var notificationApiCallbackHttpClient = new HttpClient();
        notificationApiCallbackHttpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", callbackToken);
        var baseUrl = _serviceConfiguration.NotificationApiUrl;
        NotificationApiCallbackClient = NotificationApiClient.GetClient(baseUrl, notificationApiCallbackHttpClient);
    }

    

    private string GenerateApiToken()
    {
        return new AzureTokenProvider(Options.Create(_azureConfiguration)).GetClientAccessToken(_azureConfiguration.ClientId,
            _azureConfiguration.ClientSecret, _serviceConfiguration.VhNotificationApiResourceId);
    }
    
    private string GenerateCallbackToken()
    {
        return new CustomJwtTokenProvider().GenerateTokenForCallbackEndpoint(_notifyConfiguration.CallbackSecret, 60);
    }

    private void RegisterSettings()
    {
        _configRoot = ConfigRootBuilder.Build();
        _azureConfiguration = _configRoot.GetSection("AzureAd").Get<AzureAdConfiguration>();
        _serviceConfiguration = _configRoot.GetSection("Services").Get<ServicesConfiguration>();
        _notifyConfiguration =  _configRoot.GetSection("NotifyConfiguration").Get<NotifyConfiguration>();
    }
}
