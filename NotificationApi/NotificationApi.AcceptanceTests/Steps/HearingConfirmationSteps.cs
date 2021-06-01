using System.Collections.Generic;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract;
using TechTalk.SpecFlow;
using Testing.Common.Extensions;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class HearingConfirmationSteps
    {
        private readonly AcTestContext _context;

        public HearingConfirmationSteps(AcTestContext context)
        {
            _context = context;
        }

        [Given(@"I have a hearing confirmation for a judge email notification request")]
        public void GivenIHaveAHearingConfirmationForAJudgeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationJudge;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judge", $"{Faker.Name.FullName()}");
            parameters.Add("courtroom account username", Faker.Internet.Email());
            _context.CreateNotificationRequest =
                AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }
        
        [Given(@"I have a hearing confirmation for an ejud judge email notification request")]
        public void GivenIHaveAHearingConfirmationForAnEjudJudgeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationEJudJudge;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judge", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest =
                AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }

        [Given(@"I have a hearing confirmation for a joh email notification request")]
        public void GivenIHaveAHearingConfirmationForAJohEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationJoh;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judicial office holder", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest =
                AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }

        [Given(@"I have a hearing confirmation for a LIP email notification request")]
        public void GivenIHaveAHearingConfirmationForALipEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationLip;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("name", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest =
                AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);

        }

        [Given(@"I have a hearing confirmation for a telephone LIP email notification request")]
        public void GivenIHaveAHearingConfirmationForATelephoneLipEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.TelephoneHearingConfirmationLip;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("name", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest =
                AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);

        }

        [Given(@"I have a hearing confirmation for a representative email notification request")]
        public void GivenIHaveAHearingConfirmationForARepresentativeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationRepresentative;
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
            };
        }
    }
}
