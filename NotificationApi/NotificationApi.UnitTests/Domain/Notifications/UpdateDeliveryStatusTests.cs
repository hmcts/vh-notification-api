using System;
using FluentAssertions;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using NUnit.Framework;

namespace NotificationApi.UnitTests.Domain.Notifications
{
    public class UpdateDeliveryStatusTests
    {
        [TestCase(DeliveryStatus.Created)]
        [TestCase(DeliveryStatus.Sending)]
        [TestCase(DeliveryStatus.Delivered)]
        [TestCase(DeliveryStatus.Failed)]
        public void should_update_delivery_status(DeliveryStatus newStatus)
        {
            const NotificationType notificationType = NotificationType.CreateIndividual;
            const string toEmail = "test@unit.com";
            var patId = Guid.NewGuid();
            var hearingId = Guid.NewGuid();
            
            var notification = new EmailNotification(Guid.NewGuid(), notificationType, toEmail, patId, hearingId);
            notification.DeliveryStatus.Should().Be(DeliveryStatus.NotSent);
            notification.UpdateDeliveryStatus(newStatus);
            notification.DeliveryStatus.Should().Be(newStatus);
        }
    }
}
