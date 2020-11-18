using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Testing.Common.Configuration;
using NotifyApi.DAL;
using NotifyApi.IntegrationTests.Helper;
using Testing.Common.Models;

namespace NotifyApi.IntegrationTests.Contexts
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
        public NotifyApiTokens Tokens { get; set; }
        public string Uri { get; set; }
        public DbContextOptions<NotifyApiDbContext> NotifyBookingsDbContextOptions { get; set; }

        public HttpClient CreateClient()
        {
            var client = Server.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Tokens.NotifyApiBearerToken}");
            return client;
        }
    }
}
