using System;

namespace NotificationApi.Contract.Responses
{
    public class NotificationTemplateResponse
    {
        public long Id { get; set; }

        public Guid NotifyemplateId { get; set; }

        public int NotificationType { get; set; }

        public string Parameters { get; set; }

    }
}
