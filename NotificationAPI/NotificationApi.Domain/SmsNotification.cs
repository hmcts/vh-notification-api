using System;
using NotificationApi.Domain.Enums;

namespace NotificationApi.Domain
{
    public sealed class SmsNotification : Notification
    {
        public override MessageType MessageType => MessageType.SMS;
        public string PhoneNumber { get; }

        private SmsNotification()
        {
        }

        public SmsNotification(NotificationType notificationType, string payload, string phoneNumber,
            Guid participantRefId, Guid hearingRefId) : base(notificationType, payload, participantRefId, hearingRefId)
        {
            PhoneNumber = phoneNumber;
        }
    }
}