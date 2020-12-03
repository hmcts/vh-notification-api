using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;
using TechTalk.SpecFlow;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class CreateNotificationSteps
    {
        private readonly AcTestContext _context;
        private AddNotificationRequest _request;
        
        public CreateNotificationSteps(AcTestContext context)
        {
            _context = context;
        }
        
        [Given(@"I have a request to create an email notification")]
        public void Given_I_Have_A_Request_To_Create_An_Email_Notification()
        {
            _request = BuildNotificationRequest(MessageType.Email);
        }
        
        [When(@"I send the create notification request")]
        public async Task WhenISendTheCreateNotificationRequest()
        {
            await _context.ExecuteApiRequest(() =>
                _context.ApiClient.CreateNewNotificationAsync(_request));
        }
        
        [Then(@"Notify should have my request")]
        public async Task ThenNotifyShouldHaveMyRequest()
        {
            var allNotifications = await _context.NotificationClient.GetNotificationsAsync("email");
            var name = _request.Parameters["name"];
            var username = _request.Parameters["username"];
            var recentNotification =
                allNotifications.notifications.LastOrDefault(x => x.body.Contains(name) && x.body.Contains(username));
            recentNotification.Should().NotBeNull();
        }
        
        private AddNotificationRequest BuildNotificationRequest(MessageType messageType)
        {
            var parameters = new Dictionary<string, string>
            {
                {"name", $"AC Test ${Guid.NewGuid().ToString()}"},
                {"username", $"{Guid.NewGuid().ToString()}@automation.com"},
                {"random password", "testpassword!"}
            };

            return new AddNotificationRequest
            {
                ContactEmail = messageType == MessageType.Email ? "email@email.com" : null,
                HearingId = Guid.NewGuid(),
                MessageType = messageType,
                NotificationType = NotificationType.CreateIndividual,
                Parameters = parameters,
                ParticipantId = Guid.NewGuid(),
                PhoneNumber = messageType == MessageType.SMS ? "01234567890" : null
            };
        }
    }
}
