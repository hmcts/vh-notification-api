using System.Collections.Generic;
using NotificationApi.Domain;

namespace Testing.Common.Models
{
    public class TestRun
    {
        public TestRun()
        {
            NotificationsCreated = new List<Notification>();
        }
        public List<Notification> NotificationsCreated { get; set; }
    }
}
