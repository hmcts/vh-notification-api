using System;
using System.Runtime.Serialization;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Exceptions
{

    [Serializable]
    public class DuplicateNotificationTemplateException : Exception
    {
        public DuplicateNotificationTemplateException(NotificationType notificationType) : base(
            $"Duplicate entry for notification type {notificationType} found")
        {
        }

        protected DuplicateNotificationTemplateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
