using System;
using System.Threading.Tasks;
using NotificationApi.Client;
using NotificationApi.Contract.Requests;
using Notify.Interfaces;
using Testing.Common.Configuration;

namespace NotificationApi.AcceptanceTests.Contexts
{
    public class AcTestContext
    {
        public Config Config { get; set; }
        public NotificationApiTokens Tokens { get; set; }
        public NotificationApiClient ApiClient { get; set; }
        public NotificationApiClient ApiCallbackClient { get; set; }
        public IAsyncNotificationClient NotifyClient { get; set; }
        public AddNotificationRequest CreateNotificationRequest { get; set; }
        public Notify.Models.Notification RecentNotification { get; set; }
        public object ApiClientResponse { get; set; }
        public string ApiClientMessage { get; set; }
        
        public async Task ExecuteApiRequest<T>(Func<Task<T>> apiFunc)
        {
            try
            {
                var result = await apiFunc();
                ApiClientResponse = result;
            }
            catch (NotificationApiException e)
            {
                ApiClientResponse = e.Response;
                ApiClientMessage = e.Message;
            }
        }
        
        public async Task ExecuteApiRequest(Func<Task> apiFunc)
        {
            try
            {
                await apiFunc();
                ApiClientResponse = true;
            }
            catch (NotificationApiException e)
            {
                ApiClientResponse = e.Response;
                ApiClientMessage = e.Message;
            }
        }
    }
}
