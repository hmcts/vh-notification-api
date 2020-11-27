using System;
using System.Runtime.Serialization;

namespace NotificationApi.DAL.Exceptions
{
    [Serializable]
    public class NotificationNotFoundException : Exception
    {
        public NotificationNotFoundException()
        {}
        public NotificationNotFoundException(Guid notificationId) : base(
            $"Notification {notificationId} does not exist")
        {
        }
        
        protected NotificationNotFoundException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class NotificationIdMismatchException : Exception
    {
        public NotificationIdMismatchException()
        { }
        public NotificationIdMismatchException(Guid notificationId, string externalId): base(
            $"ExternalId {externalId} does not belong to Notification {notificationId}")
        {
        }
        
        protected NotificationIdMismatchException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
