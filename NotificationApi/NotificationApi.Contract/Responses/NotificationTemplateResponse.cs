using System;

namespace NotificationApi.Contract.Responses
{
    public class NotificationTemplateResponse
    {
        public Guid Id { get; set; }

        public Guid NotificationTemplateId { get; set; }
        public int NotificationType { get; set; }

        public string Parameters { get; set; }

    }
}
