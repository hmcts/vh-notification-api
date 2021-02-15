using System;
using System.Collections.Generic;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;
using TechTalk.SpecFlow;
using Testing.Common.Extensions;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class CreateNotificationSteps
    {
        private readonly AcTestContext _context;
        
        public CreateNotificationSteps(AcTestContext context)
        {
            _context = context;
        }
        
        [Given(@"I have a request to create an email notification for new individual")]
        public void Given_I_Have_A_Request_To_Create_An_Email_Notification_For_New_Individual()
        {
            _context.CreateNotificationRequest = BuildNewIndividualNotificationRequest(MessageType.Email);
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
            _context.CreateNotificationRequest = AddNotificationRequestBuilder.BuildNonHearingRequest(messageType, templateType, parameters);
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
