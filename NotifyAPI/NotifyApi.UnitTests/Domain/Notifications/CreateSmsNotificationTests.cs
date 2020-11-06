using System;
using FluentAssertions;
using NotifyApi.Domain;
using NotifyApi.Domain.Enums;
using NUnit.Framework;

namespace NotifyApi.UnitTests.Domain.Notifications
{
    public class CreateSmsNotificationTests
    {
        [Test]
        public void should_create_notification_with_sms_type_and_phone_number()
        {
            const NotificationType notificationType = NotificationType.CreateUser;
            const MessageType messageType = MessageType.SMS;
            const string payload = "{name:first}";
            const string phoneNumber = "123456789";
            var patId = Guid.NewGuid();
            var hearingId = Guid.NewGuid();
            
            var notification = new SmsNotification(notificationType, payload, phoneNumber, patId, hearingId);

            notification.Id.Should().NotBeEmpty();
            notification.PhoneNumber.Should().Be(phoneNumber);
            notification.Payload.Should().Be(payload);
            notification.ParticipantRefId.Should().Be(patId);
            notification.HearingRefId.Should().Be(hearingId);
            notification.DeliveryStatus.Should().Be(DeliveryStatus.NotSent);
            notification.MessageType.Should().Be(messageType);
            notification.NotificationType.Should().Be(notificationType);
        }
    }
}
