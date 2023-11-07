using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Exceptions;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.IntegrationTests.Database.Commands
{
    public class UpdateNotificationDeliveryStatusCommandTests : DatabaseTestsBase
    {
        private readonly List<Notification> _notifications = new List<Notification>();
        private UpdateNotificationDeliveryStatusCommandHandler _handler;
        
        [SetUp]
        public void Setup()
        {
            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            _handler = new UpdateNotificationDeliveryStatusCommandHandler(context);
        }
        
        [Test]
        public async Task should_update_delivery_status_for_notification()
        {
            // Arrange
            var notification = await TestDataManager.SeedSendingNotification();
            _notifications.Add(notification);
            const DeliveryStatus deliveryStatus = DeliveryStatus.Delivered;
            var command = new UpdateNotificationDeliveryStatusCommand(notification.Id, notification.ExternalId, deliveryStatus);

            // Act
            Thread.Sleep(1000); // wait for a second
            await _handler.Handle(command);

            // Assert
            await using var db = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            var updatedNotification = await db.Notifications.SingleOrDefaultAsync(x => x.Id == notification.Id);
            updatedNotification.Should().NotBeNull();
            updatedNotification.DeliveryStatus.Should().Be(deliveryStatus);
            updatedNotification.CreatedAt.Should().Be(notification.CreatedAt.Value);
            updatedNotification.UpdatedAt.Should().BeAfter(updatedNotification.CreatedAt.Value);
        }

        [Test]
        public void should_throw_not_found_exception_when_id_does_not_exist()
        {
            var command =
                new UpdateNotificationDeliveryStatusCommand(Guid.NewGuid(), "1234", DeliveryStatus.Delivered);
            Assert.ThrowsAsync<NotificationNotFoundException>(() => _handler.Handle(command));
        }
        
        [Test]
        public async Task should_throw_mismatch_id_exception_when_id_and_external_id_do_not_match()
        {
            var notification = await TestDataManager.SeedSendingNotification();
            _notifications.Add(notification);
            
            var command =
                new UpdateNotificationDeliveryStatusCommand(notification.Id, "1234", DeliveryStatus.Delivered);
            Assert.ThrowsAsync<NotificationIdMismatchException>(() => _handler.Handle(command));
        }

        [TearDown]
        public async Task TearDown()
        {
            await TestDataManager.RemoveNotifications(_notifications.Select(x=> x.Id));
            _notifications.Clear();
        }
    }
}
