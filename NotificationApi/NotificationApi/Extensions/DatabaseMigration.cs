using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotificationApi.DAL;

namespace NotificationApi.Extensions
{
    public static class DatabaseMigration
    {
        public static void RunLatestMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var ctx = serviceScope.ServiceProvider.GetService<NotificationsApiDbContext>();
                ctx.Database.Migrate();
            }
        }
        
        public static void RunTemplateDataSeeding(this IApplicationBuilder app, string environment)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var ctx = serviceScope.ServiceProvider.GetService<NotificationsApiDbContext>();
                new TemplateDataSeeding(ctx).Run(environment);
            }
        }
    }
}
