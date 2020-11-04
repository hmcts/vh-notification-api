using Notify.API;
using NotifyApi.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using NotifyApi.Common.Configuration;

namespace NotifyApi.IntegrationTests.Database
{
    public abstract class DatabaseTestsBase
    {
        private string _databaseConnectionString;
        private ServicesConfiguration _services;
        protected DbContextOptions<NotifyApiDbContext> NotifyBookingsDbContextOptions;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var configRootBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddUserSecrets<Startup>();

            var configRoot = configRootBuilder.Build();
            _databaseConnectionString = configRoot.GetConnectionString("VhNotifyApi");
            _services = Options.Create(configRoot.GetSection("Services").Get<ServicesConfiguration>()).Value;

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<NotifyApiDbContext>();
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString);
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            NotifyBookingsDbContextOptions = dbContextOptionsBuilder.Options;

            var context = new NotifyApiDbContext(NotifyBookingsDbContextOptions);
            context.Database.Migrate();
        }
    }
}
