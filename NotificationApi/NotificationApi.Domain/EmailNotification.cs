using System;
using NotificationApi.Domain.Enums;

namespace NotificationApi.Domain
{
    public sealed class EmailNotification : Notification
    {
        public override MessageType MessageType => MessageType.Email;
        public string ToEmail { get; }

        private EmailNotification()
        {
        }

        public EmailNotification(Guid id, NotificationType notificationType, string toEmail,
            Guid participantRefId, Guid hearingRefId) : base(id, notificationType, participantRefId, hearingRefId)
        {
            ToEmail = toEmail;
        }
    }
}
