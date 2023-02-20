using NotificationApi.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotificationApi.IntegrationTests.Helper;
using NUnit.Framework;
using Testing.Common.Configuration;

namespace NotificationApi.IntegrationTests.Database
{
    public abstract class DatabaseTestsBase
    {
        private string _databaseConnectionString;
        protected DbContextOptions<NotificationsApiDbContext> NotifyBookingsDbContextOptions;
        protected TestDataManager TestDataManager;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var configRoot = ConfigRootBuilder.Build();
            _databaseConnectionString = configRoot.GetConnectionString("VhNotificationsApi");
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<NotificationsApiDbContext>();
            dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString);

            NotifyBookingsDbContextOptions = dbContextOptionsBuilder.Options;
            TestDataManager = new TestDataManager(NotifyBookingsDbContextOptions);

            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            context.Database.Migrate();
            new TemplateDataSeeding(context).Run("Dev");
        }
    }
}
