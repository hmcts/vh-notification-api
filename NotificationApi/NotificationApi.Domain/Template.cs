using System;
using NotificationApi.Domain.Enums;

namespace NotificationApi.Domain;

public class Template(
    Guid notifyTemplateId,
    NotificationType notificationType,
    MessageType messageType,
    string parameters)
    : TrackableEntity<long>
{
    public Guid NotifyTemplateId { get; } = notifyTemplateId;
    public NotificationType NotificationType { get; } = notificationType;
    public MessageType MessageType { get; } = messageType;
    public string Parameters { get; } = parameters;
    
    public Template(Guid notifyTemplateId, NotificationType notificationType, MessageType messageType,
        string parameters, DateTime createdAt, DateTime updatedAt): this(notifyTemplateId, notificationType, messageType, parameters)
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
