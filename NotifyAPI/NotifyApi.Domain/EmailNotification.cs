using System;
using NotifyApi.Domain.Enums;

namespace NotifyApi.Domain
{
    public sealed class EmailNotification : Notification
    {
        public override MessageType MessageType => MessageType.Email;
        public string ToEmail { get; }

        private EmailNotification()
        {
        }

        public EmailNotification(NotificationType notificationType, string payload, string toEmail,
            Guid participantRefId, Guid hearingRefId) : base(notificationType, payload, participantRefId, hearingRefId)
        {
            ToEmail = toEmail;
        }
    }
}
