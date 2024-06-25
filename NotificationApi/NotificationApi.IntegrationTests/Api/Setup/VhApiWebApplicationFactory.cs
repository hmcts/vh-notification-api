using System.Net.Http;
using GST.Fake.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace NotificationApi.IntegrationTests.Api.Setup
{
    public class VhApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        private static readonly string[] Roles = { "ROLE_ADMIN", "ROLE_GENTLEMAN" };
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                }).AddFakeJwtBearer();
                
                RegisterStubs(services);
            });
            builder.UseEnvironment("Development");
        }
        
        private static void RegisterStubs(IServiceCollection services)
        {
            services.AddSingleton<IAsyncNotificationClient, AsyncNotificationClientStub>();
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
            client.SetFakeBearerToken("admin", Roles);
        }
    }
}
