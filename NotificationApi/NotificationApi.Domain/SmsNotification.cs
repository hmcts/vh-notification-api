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

        public SmsNotification(Guid id, NotificationType notificationType, string phoneNumber,
            Guid participantRefId, Guid hearingRefId) : base(id, notificationType, participantRefId, hearingRefId)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
