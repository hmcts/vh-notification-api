using System;
using System.Runtime.Serialization;

namespace NotificationApi.DAL.Exceptions
{
    public abstract class NotificationDalException : Exception
    {
        protected NotificationDalException(string message) : base(message)
        {
        }
        
        protected NotificationDalException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class NotificationNotFoundException : NotificationDalException
    {
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
    public class NotificationIdMismatchException : NotificationDalException
    {
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
