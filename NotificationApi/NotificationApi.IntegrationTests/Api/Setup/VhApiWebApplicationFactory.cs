using System.Linq;
using System.Net.Http;
using GST.Fake.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotificationApi.DAL;
using NotificationApi.IntegrationTests.Stubs;
using Notify.Interfaces;

namespace NotificationApi.IntegrationTests.Api.Setup
{
    public class VhApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var authorizationDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IAuthorizationHandler));
                if (authorizationDescriptor != null)
                    services.Remove(authorizationDescriptor);
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                }).AddFakeJwtBearer();
                services.AddScoped<IAsyncNotificationClient, AsyncNotificationClientStub>();
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<NotificationsApiDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<NotificationsApiDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                }, ServiceLifetime.Singleton);
            });
            builder.UseEnvironment("Development");
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
            client.SetFakeBearerToken("admin", new[] { "ROLE_ADMIN", "ROLE_GENTLEMAN" });
        }
    }
}
