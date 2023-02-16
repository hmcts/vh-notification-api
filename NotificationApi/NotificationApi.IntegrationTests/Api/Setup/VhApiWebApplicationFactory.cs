using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationApi.DAL;
using NotificationApi.IntegrationTests.Stubs;
using NotificationApi.Services;
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
        });
        
        builder.ConfigureServices(services =>
        {
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
        // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        base.ConfigureClient(client);
    }
}
