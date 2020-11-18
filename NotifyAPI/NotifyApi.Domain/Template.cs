using System;
using NotifyApi.Domain.Ddd;
using NotifyApi.Domain.Enums;

namespace NotifyApi.Domain
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
