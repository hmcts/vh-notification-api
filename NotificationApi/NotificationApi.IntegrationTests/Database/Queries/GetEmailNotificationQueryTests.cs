using System;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.DAL;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Queries;
using NotificationApi.Domain.Enums;
using NUnit.Framework;

namespace NotificationApi.IntegrationTests.Database.Queries
{
    public class GetEmailNotificationQueryTests : DatabaseTestsBase
    {
        private GetEmailNotificationQueryHandler _handler;
        private CreateEmailNotificationCommandHandler _commandHandler;

        [SetUp]
        public void Setup()
        {
            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            _handler = new GetEmailNotificationQueryHandler(context);
            _commandHandler = new CreateEmailNotificationCommandHandler(context);
        }

        [Test]
        public async Task should_get_notification_when_exists()
        {
            var notificationType = NotificationType.CreateIndividual;
            const string email = "test@hmcts.net";
            var participantId = Guid.NewGuid();
            var hearingId = Guid.NewGuid();
            var command = new CreateEmailNotificationCommand(notificationType, email, participantId, hearingId, "scheduledDateTime:2022-06-19T16:16:38.024Z");
            await _commandHandler.Handle(command);

            var query = new GetEmailNotificationQuery(hearingId, participantId, notificationType, email);
            var result = await _handler.Handle(query);
            result.Should().NotBeNull();
        }
    }
}
