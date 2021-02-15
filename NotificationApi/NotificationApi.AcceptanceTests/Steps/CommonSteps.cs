using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.AcceptanceTests.Contexts;
using Notify.Models;
using TechTalk.SpecFlow;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public sealed class CommonSteps
    {
        private readonly AcTestContext _context;

        public CommonSteps(AcTestContext acTestContext)
        {
            _context = acTestContext;
        }

        [When(@"I send the request to the endpoint")]
        [When(@"I resend the request to the endpoint")]
        public void WhenISendTheRequestToTheEndpoint()
        {
            _context.Response = _context.Client().Execute(_context.Request);
        }

        [Then(@"the response should have the status (.*) and success status (.*)")]
        public void ThenTheResponseShouldHaveTheStatusAndSuccessStatus(HttpStatusCode httpStatusCode, bool isSuccess)
        {
            _context.Response.StatusCode.Should().Be(httpStatusCode);
            _context.Response.IsSuccessful.Should().Be(isSuccess);
        }
        
        [Then(@"the api client should return true")]
        public void ThenApiClientShouldReturnTrue()
        {
            _context.ApiClientResponse.Should().Be(true);
        }
        
        [When(@"I send the create notification request")]
        public async Task WhenISendTheCreateNotificationRequest()
        {
            await _context.ExecuteApiRequest(() =>
                _context.ApiClient.CreateNewNotificationAsync(_context.CreateNotificationRequest));
        }
        
        [Then(@"Notify should have my request")]
        public async Task ThenNotifyShouldHaveMyRequest()
        {
            await AssertNotifyHasMyRequest(notification =>
            {
                foreach (var parameter in _context.CreateNotificationRequest.Parameters)
                {
                    return notification.body.Contains(parameter.Value);
                }

                return false;
            });
        }
        
        private async Task AssertNotifyHasMyRequest(Func<Notification, bool> predicate)
        {
            var allNotifications = await _context.NotifyClient.GetNotificationsAsync("email");
            _context.RecentNotification = allNotifications.notifications.LastOrDefault(predicate);
            _context.RecentNotification.Should().NotBeNull();
        }
    }
}
