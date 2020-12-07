using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Exceptions;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using NUnit.Framework;

namespace NotificationApi.IntegrationTests.Database.Commands
{
    public class UpdateNotificationSentCommandHandlerTests : DatabaseTestsBase
    {
        private readonly List<Notification> _notifications = new List<Notification>();
        private UpdateNotificationSentCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            _handler = new UpdateNotificationSentCommandHandler(context);
        }

        [Test]
        public async Task Should_Update_Delivery_Status_To_Created()
        {
            var notification = await TestDataManager.SeedSendingNotification();
            _notifications.Add(notification);
            const DeliveryStatus expectedDeliveryStatus = DeliveryStatus.Created;
            var command = new UpdateNotificationSentCommand(notification.Id, notification.ExternalId, notification.Payload);

            await _handler.Handle(command);
            
            await using var db = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            var updatedNotification = await db.Notifications.SingleOrDefaultAsync(x => x.Id == notification.Id);
            updatedNotification.Should().NotBeNull();
            updatedNotification.DeliveryStatus.Should().Be(expectedDeliveryStatus);
        }
        
        [Test]
        public void Should_Throw_Not_Found_Exception_When_Id_Does_Not_Exist()
        {
            var command =
                new UpdateNotificationSentCommand(Guid.NewGuid(), "1234", "payload");
            Assert.ThrowsAsync<NotificationNotFoundException>(() => _handler.Handle(command));
        }
        
        [Test]
        public async Task Should_Assign_ExternalId_And_Payload()
        {
            var notification = await TestDataManager.SeedSendingNotification();
            _notifications.Add(notification);
            const string expectedExternalId = "1234";
            const string expectedPayload = "payload";
            notification.AssignExternalId(expectedExternalId);
            notification.AssignPayload(expectedPayload);
            var command = new UpdateNotificationSentCommand(notification.Id, notification.ExternalId, notification.Payload);

            await _handler.Handle(command);
            
            await using var db = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            var updatedNotification = await db.Notifications.SingleOrDefaultAsync(x => x.Id == notification.Id);
            updatedNotification.Payload.Should().Be(expectedPayload);
            updatedNotification.ExternalId.Should().Be(expectedExternalId);
        }

        [TearDown]
        public async Task TearDown()
        {
            await TestDataManager.RemoveNotifications(_notifications.Select(x=> x.Id));
            _notifications.Clear();
        }
    }
}
