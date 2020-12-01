using System;
using System.Collections.Generic;

namespace NotificationApi.Contract.Requests
{
    /// <summary>
    /// Add a new notification
    /// </summary>
    public class AddNotificationRequest
    {
        public string ContactEmail { get; set; }
        public int NotificationType { get; set; }
        public string PhoneNumber { get; set; }
        public Guid HearingId { get; set; }
        public Guid ParticipantId { get; set; }
        public int MessageType { get; set; }
        
        /// <summary>
        /// Includes data for the hearing such as Case Number, Name, Date, Time and Username
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }
    }
}
