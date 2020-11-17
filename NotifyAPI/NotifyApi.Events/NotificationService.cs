using Notify.Interfaces;

namespace NotifyApi.Events
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
