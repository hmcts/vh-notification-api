using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotificationApi.DAL;
using NotificationApi.IntegrationTests.Stubs;
using Notify.Interfaces;

namespace NotificationApi.IntegrationTests.Api.Setup;

public class VhApiWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _token;
    public DbContextOptions<NotificationsApiDbContext> NotificationApiDbContextOptions { get; private set; }

    public VhApiWebApplicationFactory(string token)
    {
        _token = token;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddScoped<IAsyncNotificationClient, AsyncNotificationClientStub>();
            services.AddMvc(options =>
            {
                options.Filters.Add(new AllowAnonymousFilter());
            });
            
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
}
