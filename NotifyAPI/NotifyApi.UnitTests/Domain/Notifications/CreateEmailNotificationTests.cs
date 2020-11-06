using System;
using FluentAssertions;
using NotifyApi.Domain;
using NotifyApi.Domain.Enums;
using NUnit.Framework;

namespace NotifyApi.UnitTests.Domain.Notifications
{
    public class CreateEmailNotificationTests
    {
        [Test]
        public void should_create_notification_with_email_type_and_email()
        {
            const NotificationType notificationType = NotificationType.CreateUser;
            const MessageType messageType = MessageType.Email;
            const string payload = "{name:first}";
            const string toEmail = "test@unit.com";
            var patId = Guid.NewGuid();
            var hearingId = Guid.NewGuid();
            
            var notification = new EmailNotification(notificationType, payload, toEmail, patId, hearingId);

            notification.Id.Should().NotBeEmpty();
            notification.ToEmail.Should().Be(toEmail);
            notification.Payload.Should().Be(payload);
            notification.ParticipantRefId.Should().Be(patId);
            notification.HearingRefId.Should().Be(hearingId);
            notification.DeliveryStatus.Should().Be(DeliveryStatus.NotSent);
            notification.MessageType.Should().Be(messageType);
            notification.NotificationType.Should().Be(notificationType);
        }
    }
}
