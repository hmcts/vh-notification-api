using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Testing.Common.Configuration;
using NotificationApi.DAL;
using NotificationApi.IntegrationTests.Helper;
using Testing.Common.Models;

namespace NotificationApi.IntegrationTests.Contexts
{
    public class IntTestContext
    {
        public IntTestContext()
        {
            TestRun= new TestRun();
        }
        public Config Config { get; set; }
        public HttpContent HttpContent { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public HttpResponseMessage Response { get; set; }
        public TestServer Server { get; set; }
        public TestRun TestRun { get; set; }
        public TestDataManager TestDataManager { get; set; }
        public NotificationApiTokens Tokens { get; set; }
        public string Uri { get; set; }
        public DbContextOptions<NotificationsApiDbContext> NotifyBookingsDbContextOptions { get; set; }

        public HttpClient CreateClient()
        {
            var client = Server.CreateClient();
            if (Uri.ToLower().Contains("callback"))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Tokens.NotificationCallbackBearerToken}");
            }
            else
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Tokens.NotificationApiBearerToken}");
            }

            return client;
        }
    }
}
