namespace NotificationApi.Contract.Requests
{
    /// <summary>
    /// Add a new notification
    /// </summary>
    public class AddNotificationRequest
    {
        public string ExternalId { get; set; }
        public string ContactEmail { get; set; }
        public int NotificationType { get; set; }
        public string PhoneNumber { get; set; }
        public string HearingId { get; set; }
        public string ParticipantId { get; set; }
        public int MessageType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
