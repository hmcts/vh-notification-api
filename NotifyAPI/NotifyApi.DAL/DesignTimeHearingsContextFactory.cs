using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NotifyApi.DAL
{
    public class DesignTimeHearingsContextFactory : IDesignTimeDbContextFactory<NotifyApiDbContext>
    {
        public NotifyApiDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("9AECE566-336D-4D16-88FA-7A76C27321CD")
                .Build();
            var builder = new DbContextOptionsBuilder<NotifyApiDbContext>();
            builder.UseSqlServer(config.GetConnectionString("VhNotifyApi"));
            var context = new NotifyApiDbContext(builder.Options);
            return context;
        }
    }

}