using System;
using FluentAssertions;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using NUnit.Framework;

namespace NotificationApi.UnitTests.Domain.Notifications
{
    public class CreateEmailNotificationTests
    {
        [Test]
        public void should_create_notification_with_email_type_and_email()
        {
            const NotificationType notificationType = NotificationType.CreateIndividual;
            const MessageType messageType = MessageType.Email;
            const string toEmail = "test@unit.com";
            var patId = Guid.NewGuid();
            var hearingId = Guid.NewGuid();
            var notificationId = Guid.NewGuid();
            var notification = new EmailNotification(notificationId, notificationType, toEmail, patId, hearingId);

            notification.Id.Should().NotBeEmpty();
            notification.ToEmail.Should().Be(toEmail);
            notification.ParticipantRefId.Should().Be(patId);
            notification.HearingRefId.Should().Be(hearingId);
            notification.DeliveryStatus.Should().Be(DeliveryStatus.NotSent);
            notification.MessageType.Should().Be(messageType);
            notification.NotificationType.Should().Be(notificationType);
        }
    }
}
