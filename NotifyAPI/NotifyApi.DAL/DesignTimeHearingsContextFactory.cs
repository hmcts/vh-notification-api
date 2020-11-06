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
                .AddUserSecrets("4E35D845-27E7-4A19-BE78-CDA896BF907D")
                .Build();
            var builder = new DbContextOptionsBuilder<NotifyApiDbContext>();
            builder.UseSqlServer(config.GetConnectionString("VhNotifyApi"));
            var context = new NotifyApiDbContext(builder.Options);
            return context;
        }
    }

}
