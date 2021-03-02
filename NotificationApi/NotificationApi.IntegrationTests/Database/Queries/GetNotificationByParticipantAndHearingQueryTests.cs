using FluentAssertions;
using NotificationApi.DAL;
using NotificationApi.DAL.Queries;
using NUnit.Framework;
using System.Threading.Tasks;

namespace NotificationApi.IntegrationTests.Database.Queries
{
    public class GetNotificationByParticipantAndHearingQueryTests : DatabaseTestsBase
    {
        private GetNotificationByParticipantAndHearingQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            _handler = new GetNotificationByParticipantAndHearingQueryHandler(context);
        }

        [Test]
        public async Task should_get_password_notification_by_email()
        {
            var notification = await TestDataManager.SeedCreatedNotification();

            var query = new GetNotificationByParticipantAndHearingQuery(notification.NotificationType, 
                                    notification.HearingRefId.ToString(), notification.ParticipantRefId.ToString());
            var result = await _handler.Handle(query);
            result.Should().NotBeNull();
            result.Id.Should().Be(notification.Id);
        }
    }
}
