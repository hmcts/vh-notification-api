using System;
using NotificationApi.Domain.Ddd;
using NotificationApi.Domain.Enums;

namespace NotificationApi.Domain
{
    public class Template : Entity<long>
    {
        public Guid NotifyTemplateId { get; }
        public NotificationType NotificationType { get; }
        public MessageType MessageType { get; }
        public string Parameters { get; }

        public Template(Guid notifyTemplateId, NotificationType notificationType, MessageType messageType, string parameters)
        {
            NotifyTemplateId = notifyTemplateId;
            NotificationType = notificationType;
            MessageType = messageType;
            Parameters = parameters;
        }
    }
}
