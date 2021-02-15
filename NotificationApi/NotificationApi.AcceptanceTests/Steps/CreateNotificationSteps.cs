using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;
using Notify.Models;
using TechTalk.SpecFlow;
using Testing.Common.Extensions;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class CreateNotificationSteps
    {
        private readonly AcTestContext _context;
        public AddNotificationRequest Request { get; private set; }
        public Notification RecentNotification { get; private set; }
        
        public CreateNotificationSteps(AcTestContext context)
        {
            _context = context;
        }
        
        [Given(@"I have a request to create an email notification for new individual")]
        public void Given_I_Have_A_Request_To_Create_An_Email_Notification_For_New_Individual()
        {
            Request = BuildNewIndividualNotificationRequest(MessageType.Email);
        }
        
        [Given(@"I have a request to create an email notification for password reset")]
        public void Given_I_Have_A_Request_To_Create_An_Email_Notification_For_Password_Reset()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.PasswordReset;
            var parameters = new Dictionary<string, string>
            {
                {"name", Faker.Name.FullName()},
                {"password", "ACTestPasswordReset!"}
            };
            Request = AddNotificationRequestBuilder.BuildNonHearingRequest(messageType, templateType, parameters);
        }
        
        [When(@"I send the create notification request")]
        public async Task WhenISendTheCreateNotificationRequest()
        {
            await _context.ExecuteApiRequest(() =>
                _context.ApiClient.CreateNewNotificationAsync(Request));
        }
        
        [Then(@"Notify should have my request")]
        public async Task ThenNotifyShouldHaveMyRequest()
        {
            var name = Request.Parameters["name"];
            var username = Request.Parameters["username"];
            var randomPassword = Request.Parameters["random password"];
            await AssertNotifyHasMyRequest(notification =>
                notification.body.Contains(name) && notification.body.Contains(username) &&
                notification.body.Contains(randomPassword));
        }
        
        [Then(@"Notify should have my password reset request")]
        public async Task ThenNotifyShouldHaveMyPasswordResetRequest()
        {
            var name = Request.Parameters["name"];
            var username = Request.Parameters["password"];
            await AssertNotifyHasMyRequest(notification =>
                notification.body.Contains(name) && notification.body.Contains(username));
        }

        private async Task AssertNotifyHasMyRequest(Func<Notification, bool> predicate)
        {
            var allNotifications = await _context.NotifyClient.GetNotificationsAsync("email");
            RecentNotification = allNotifications.notifications.LastOrDefault(predicate);
            RecentNotification.Should().NotBeNull();
        }

        private AddNotificationRequest BuildNewIndividualNotificationRequest(MessageType messageType)
        {
            var parameters = new Dictionary<string, string>
            {
                {"name", $"{Faker.Name.FullName()}"},
                {"username", $"{Guid.NewGuid().ToString()}@automation.com"},
                {"random password", "testpassword!"}
            };

            return AddNotificationRequestBuilder.BuildRequest(messageType, NotificationType.CreateIndividual,
                parameters);
        }
    }
}
