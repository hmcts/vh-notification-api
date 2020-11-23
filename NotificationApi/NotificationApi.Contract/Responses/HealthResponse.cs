namespace NotificationApi.Contract.Responses
{
    public class HealthResponse
    {
        public HealthCheck DatabaseHealth { get; set; }
        public AppVersionResponse AppVersion { get; set; }

        public HealthResponse()
        {
            DatabaseHealth = new HealthCheck();
            AppVersion = new AppVersionResponse();
        }
    }
}
