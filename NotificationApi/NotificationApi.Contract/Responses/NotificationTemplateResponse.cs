using System;

namespace NotificationApi.Contract.Responses
{
    public class NotificationTemplateResponse
    {
        public long Id { get; set; }

        public Guid NotifyTemplateId { get; set; }

        public int NotificationType { get; set; }

        public string Parameters { get; set; }

    }
}
