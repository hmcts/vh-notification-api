using System;
using System.Collections.Generic;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;
using NotificationApi.IntegrationTests.Contexts;
using TechTalk.SpecFlow;
using Testing.Common.Extensions;

namespace NotificationApi.IntegrationTests.Steps
{
    [Binding]
    public class CreateNotificationsSteps : BaseSteps
    {
        private readonly IntTestContext _context;

        public CreateNotificationsSteps(IntTestContext context)
        {
            _context = context;
        }

        [Given("I have a valid new user email notification request")]
        public void I_have_a_valid_create_new_email_notification_request()
        {
            var request = BuildNewUserNotificationRequest(MessageType.Email);
            InitCreateNotificationRequest(request, _context);
        }

        [Given("I have a valid password reset email notification request")]
        public void I_have_a_valid_password_reset_email_notification_request()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.PasswordReset;
            var parameters = new Dictionary<string, string>
            {
                {"name", $"{Faker.Name.FullName()}"},
                {"password", "inttestpassword!"}
            };
            var request = AddNotificationRequestBuilder.BuildNonHearingRequest(messageType, templateType, parameters);
            InitCreateNotificationRequest(request, _context);
        }

        private AddNotificationRequest BuildNewUserNotificationRequest(MessageType messageType)
        {
            var parameters = new Dictionary<string, string>
            {
                {"name", $"{Faker.Name.FullName()}"},
                {"username", $"{Guid.NewGuid().ToString()}@intautomation.com"},
                {"random password", "inttestpassword!"}
            };

            return AddNotificationRequestBuilder.BuildRequest(messageType, NotificationType.CreateIndividual,
                parameters);
        }
    }
}
