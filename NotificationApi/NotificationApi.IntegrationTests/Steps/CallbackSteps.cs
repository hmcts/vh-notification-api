using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using AcceptanceTests.Common.Api.Helpers;
using NotificationApi.Contract.Requests;
using NotificationApi.IntegrationTests.Contexts;
using TechTalk.SpecFlow;
using static Testing.Common.Helper.ApiUriFactory;

namespace NotificationApi.IntegrationTests.Steps
{
    [Binding]
    public class CallbackSteps : BaseSteps
    {
        private readonly IntTestContext _context;

        public CallbackSteps(IntTestContext context)
        {
            _context = context;
        }

        [Given(@"I have a update notification request with a status (.*)")]
        public void GivenIHaveAUpdateNotificationWithDeliveryStatus(string deliveryStatus)
        {
            var request = BuildRequest(deliveryStatus);
            _context.Uri = NotificationEndpoints.UpdateNotification;
            _context.HttpMethod = HttpMethod.Post;
            var jsonBody = RequestHelper.Serialise(request);
            _context.HttpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }
        
        [Given(@"I have a update notification request with an invalid status")]
        public void GivenIHaveUpdateRequestWithInvalidStatus()
        {
            var notification = _context.TestRun.NotificationsCreated.Last();
            var request = new NotificationCallbackRequest
            {
                Id = Guid.NewGuid().ToString(),
                Reference = notification.Id.ToString(),
                Status = "failed"
            };
            _context.Uri = NotificationEndpoints.UpdateNotification;
            _context.HttpMethod = HttpMethod.Post;
            var jsonBody = RequestHelper.Serialise(request);
            _context.HttpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }
        
        [Given(@"I have a update notification request with mismatched ids")]
        public void GivenIHaveUpdateRequestWithMismatchedIds()
        {
            var notification = _context.TestRun.NotificationsCreated.Last();
            var request = new NotificationCallbackRequest
            {
                Id = Guid.NewGuid().ToString(),
                Reference = notification.Id.ToString(),
                Status = "permanent-failure"
            };
            _context.Uri = NotificationEndpoints.UpdateNotification;
            _context.HttpMethod = HttpMethod.Post;
            var jsonBody = RequestHelper.Serialise(request);
            _context.HttpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }
        
        private NotificationCallbackRequest BuildRequest(string deliveryStatus)
        {
            var notification = _context.TestRun.NotificationsCreated.Last();
            return new NotificationCallbackRequest
            {
                Id = notification.ExternalId,
                Reference = notification.Id.ToString(),
                Status = deliveryStatus
            };
        }
    }
}
