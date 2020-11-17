using Notify.API;
using NotifyApi.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using NotifyApi.Common.Configuration;
using NotifyApi.IntegrationTests.Helper;

namespace NotifyApi.IntegrationTests.Database
{
    public abstract class DatabaseTestsBase
    {
        private string _databaseConnectionString;
        private ServicesConfiguration _services;
        protected DbContextOptions<NotifyApiDbContext> NotifyBookingsDbContextOptions;
        protected TestDataManager TestDataManager;

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
            dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString);
            NotifyBookingsDbContextOptions = dbContextOptionsBuilder.Options;

            var context = new NotifyApiDbContext(NotifyBookingsDbContextOptions);
            context.Database.Migrate();
            
            TestDataManager = new TestDataManager(_services, NotifyBookingsDbContextOptions);
        }
    }
}
