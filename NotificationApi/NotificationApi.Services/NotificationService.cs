using Notify.Interfaces;

namespace NotificationApi.Services
{
    public interface INotificationService
    {
    }
    public class NotificationService : INotificationService
    {
        private readonly IAsyncNotificationClient _govNotifyApiClient;

        public NotificationService(IAsyncNotificationClient govNotifyApiClient)
        {
            _govNotifyApiClient = govNotifyApiClient;
        }
    }
}
