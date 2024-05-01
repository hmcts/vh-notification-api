using System.Net.Http;
using GST.Fake.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NotificationApi.Common.Util;
using Testing.Common.Stubs;

namespace NotificationApi.IntegrationTests.Api.Setup
{
    public class VhApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var fakeJwtBearerScheme = "Fake Bearer";
                
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = fakeJwtBearerScheme;
                    options.DefaultAuthenticateScheme = fakeJwtBearerScheme;
                    options.DefaultChallengeScheme = fakeJwtBearerScheme;
                }).AddFakeJwtBearer(fakeJwtBearerScheme, options => _ = options);
                
                RegisterStubs(services);
            });
            builder.UseEnvironment("Development");
        }
        
        private static void RegisterStubs(IServiceCollection services)
        {
            services.AddSingleton<IAsyncNotificationClient, AsyncNotificationClientStub>();
            services.AddSingleton<IFeatureToggles, FeatureTogglesStub>();
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
            client.SetFakeBearerToken("admin", ["ROLE_ADMIN"]);
        }
    }
}
