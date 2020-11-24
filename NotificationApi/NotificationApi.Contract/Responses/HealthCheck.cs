using System.Collections;

namespace NotificationApi.Contract.Responses
{
    public class HealthCheck
    {
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }
        public IDictionary Data { get; set; }
    }
}
