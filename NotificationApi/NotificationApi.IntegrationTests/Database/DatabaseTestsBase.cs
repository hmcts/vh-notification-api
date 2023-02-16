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
        protected DbContextOptions<NotificationsApiDbContext> NotifyBookingsDbContextOptions;
        protected TestDataManager TestDataManager;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var configRoot = ConfigRootBuilder.Build();
            configRoot.GetConnectionString("VhNotificationsApi");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<NotificationsApiDbContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase("InMemoryDbForTesting");
            
            NotifyBookingsDbContextOptions = dbContextOptionsBuilder.Options;
            TestDataManager = new TestDataManager(NotifyBookingsDbContextOptions);
            
            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            if (context.Database.IsRelational())
            {
                context.Database.Migrate();
            }

            new TemplateDataSeeding(context).Run("Dev");
        }
    }
}
