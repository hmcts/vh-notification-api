using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotificationApi.DAL;

namespace Notification.API.Extensions
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
    }
}
