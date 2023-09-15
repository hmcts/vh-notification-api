using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NotificationApi.DAL;

namespace NotificationApi.Health;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddVhHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDbContextCheck<NotificationsApiDbContext>("Database VhNotificationsApi", tags: new[] {"startup", "readiness"});
            
        return services;
    }
}
