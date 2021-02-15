using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.DAL;
using NotificationApi.DAL.Queries;
using NotificationApi.Domain.Enums;
using NUnit.Framework;

namespace NotificationApi.IntegrationTests.Database.Queries
{
    public class GetTemplateByNotificationTypeQueryTests : DatabaseTestsBase
    {
        private GetTemplateByNotificationTypeQueryHandler _handler;
        
        [SetUp]
        public void Setup()
        {
            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            _handler = new GetTemplateByNotificationTypeQueryHandler(context);
        }

        [TestCase(NotificationType.CreateIndividual)]
        [TestCase(NotificationType.CreateRepresentative)]
        [TestCase(NotificationType.PasswordReset)]
        public async Task should_get_template_for_notification_type(NotificationType notificationType)
        {
            var query = new GetTemplateByNotificationTypeQuery(notificationType);
            var result = await _handler.Handle(query);
            result.Should().NotBeNull();
        }
    }
}
