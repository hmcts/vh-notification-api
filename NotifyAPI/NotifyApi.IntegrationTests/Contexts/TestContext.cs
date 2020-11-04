using System.Net.Http;
using AcceptanceTests.Common.Api;
using AcceptanceTests.Common.AudioRecordings;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Testing.Common.Configuration;
using NotifyApi.DAL;

namespace NotifyApi.IntegrationTests.Contexts
{
    public class TestContext
    {
        public Config Config { get; set; }
        public HttpContent HttpContent { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public HttpResponseMessage Response { get; set; }
        public TestServer Server { get; set; }
        public NotifyApiTokens Tokens { get; set; }
        public string Uri { get; set; }
        public DbContextOptions<NotifyApiDbContext> NotifyBookingsDbContextOptions { get; set; }
        public AzureStorageManager AzureStorage { get; set; }

        public HttpClient CreateClient()
        {
            HttpClient client;
            if (Zap.SetupProxy)
            {
                var handler = new HttpClientHandler
                {
                    Proxy = Zap.WebProxy,
                    UseProxy = true,
                };

                client = new HttpClient(handler)
                {
                    BaseAddress = new System.Uri(Config.VhServices.NotifyApiUrl)
                };
            }
            else
            {
                client = Server.CreateClient();
            }
            
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Tokens.NotifyApiBearerToken}");
            return client;
        }
    }
}
