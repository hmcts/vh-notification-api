using FluentAssertions;
using NotificationApi.DAL;
using NotificationApi.DAL.Queries;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.IntegrationTests.Database.Queries
{
    public class GetNotificationByEmailQueryTests : DatabaseTestsBase
    {
        private GetNotificationByEmailQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            _handler = new GetNotificationByEmailQueryHandler(context);
        }

        [Test]
        public async Task should_get_password_notification_by_email()
        {
            var email = "email@hmcts.net";
            var notification = await TestDataManager.SeedPasswordNotification(email);

            var query = new GetNotificationByEmailQuery(email);
            var result = await _handler.Handle(query);
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
            result.Any(r => r.Id == notification.Id).Should().BeTrue();
        }
    }
}
