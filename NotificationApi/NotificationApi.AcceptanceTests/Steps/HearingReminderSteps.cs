using System.Collections.Generic;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract;
using TechTalk.SpecFlow;
using Testing.Common.Extensions;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class HearingReminderSteps
    {
        private readonly AcTestContext _context;

        public HearingReminderSteps(AcTestContext context)
        {
            _context = context;
        }

        [Given(@"I have a hearing reminder for a joh email notification request")]
        public void GivenIHaveAHearingReminderForAJohEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingReminderJoh;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judicial office holder", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest =
                AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }

        [Given(@"I have a hearing reminder for a LIP email notification request")]
        public void GivenIHaveAHearingReminderForALipEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingReminderLip;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("name", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest =
                AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }

        [Given(@"I have a hearing reminder for a representative email notification request")]
        public void GivenIHaveAHearingReminderForARepresentativeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingReminderRepresentative;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("solicitor name", $"{Faker.Name.FullName()}");
            parameters.Add("client name", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest =
                AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }

        private Dictionary<string, string> InitGenericAmendmentParams()
        {
            return new Dictionary<string, string>
            {
                {"case number", "UFGFUD/1344"},
                {"case name", "Random Int Test"},
                {"time", "1:10 PM"},
                {"day month year", "12 October 2020"},
                {"username", Faker.Internet.Email()},
            };
        }
    }
}
