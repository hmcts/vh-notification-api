namespace NotificationApi.Contract.Responses
{
    public class HealthResponse
    { public AppVersionResponse Version { get; set; }
        public HealthResponse()
        {
            Version = new AppVersionResponse();
        }
    }
}
