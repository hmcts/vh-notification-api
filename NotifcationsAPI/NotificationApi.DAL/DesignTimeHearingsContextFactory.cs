using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NotificationApi.DAL
{
    public class DesignTimeHearingsContextFactory : IDesignTimeDbContextFactory<NotificationsApiDbContext>
    {
        public NotificationsApiDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("4E35D845-27E7-4A19-BE78-CDA896BF907D")
                .Build();
            var builder = new DbContextOptionsBuilder<NotificationsApiDbContext>();
            builder.UseSqlServer(config.GetConnectionString("VhNotifyApi"));
            var context = new NotificationsApiDbContext(builder.Options);
            return context;
        }
    }

}
