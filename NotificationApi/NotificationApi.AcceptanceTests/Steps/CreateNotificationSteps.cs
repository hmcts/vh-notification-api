using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        private AddNotificationRequest BuildNotificationRequest(MessageType messageType)
        {
            var parameters = new Dictionary<string, string>
            {
                {"Name", "test"},
                {"Day Month Year", "12/12/11"},
                {"time", "DateTime"},
                {"Username", "testUser"},
                {"random password", "testpass"}
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
