using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL;
using NotificationApi.DAL.Commands;
using NotificationApi.Domain.Enums;
using NUnit.Framework;

namespace NotificationApi.IntegrationTests.Database.Commands
{
    public class CreateEmailNotificationCommandTests : DatabaseTestsBase
    {
        private CreateEmailNotificationCommandHandler _handler;
        private Guid _notificationId;

        [SetUp]
        public void SetUp()
        {
            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            _handler = new CreateEmailNotificationCommandHandler(context);
        }

        [Test]
        public async Task Should_save_new_notification()
        {
            var notificationType = NotificationType.CreateIndividual;
            const string email = "test@email.com";
            var participantId = Guid.NewGuid();
            var hearingId = Guid.NewGuid();
            
            var command = new CreateEmailNotificationCommand(notificationType, email, participantId, hearingId);
            
            await _handler.Handle(command);
            await using (var db = new NotificationsApiDbContext(NotifyBookingsDbContextOptions))
            {
                await db.Notifications.SingleOrDefaultAsync(x => x.Id == command.NotificationId);
            }

            command.NotificationId.Should().NotBeEmpty();
            _notificationId = command.NotificationId;
        }

        [TearDown]
        public async Task TearDown()
        {
            if (_notificationId != Guid.Empty)
            {
                TestContext.WriteLine($"Removing test conference {_notificationId}");
                await TestDataManager.RemoveNotification(_notificationId);
            }
        }
    }
}
 
