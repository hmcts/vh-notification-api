namespace NotificationApi.Contract.Requests
{
    public class NotificationCallbackRequest
    {
        /// <summary>
        /// The external UUID of a notification provided by Notify
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The reference value provided by Notification API (i.e. the internal notification ID)
        /// </summary>
        public string Reference { get; set; }
        
        /// <summary>
        /// The new delivery status
        /// </summary>
        public string Status { get; set; }
    }
}
