using System.Collections.Generic;
using NotificationApi.Contract;
using NotificationApi.IntegrationTests.Contexts;
using TechTalk.SpecFlow;
using Testing.Common.Extensions;

namespace NotificationApi.IntegrationTests.Steps
{
    [Binding]
    public class HearingConfirmationSteps : BaseSteps
    {
        private readonly IntTestContext _context;

        public HearingConfirmationSteps(IntTestContext context)
        {
            _context = context;
        }
        
        [Given(@"I have a hearing confirmation for a judge email notification request")]
        public void GivenIHaveAHearingConfirmationForAJudgeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationJudge;
            var parameters = InitGenericConfirmationParams();
            parameters.Add("judge", $"{Faker.Name.FullName()}");
            parameters.Add("courtroom account username", Faker.Internet.Email());
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }

        [Given(@"I have a hearing confirmation for a staffmember email notification request")]
        public void GivenIHaveAHearingConfirmationForAStaffMemberEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationStaffMember;
            var parameters = InitGenericConfirmationParams();
            parameters.Add("staff member", Faker.Name.Last());
            parameters.Add("username", Faker.Internet.Email());
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);

            InitCreateNotificationRequest(request, _context);
        }

        [Given(@"I have a hearing confirmation for an ejud judge email notification request")]
        public void GivenIHaveAHearingConfirmationForAnEjudJudgeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationEJudJudge;
            var parameters = InitGenericConfirmationParams();
            parameters.Add("judge", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a hearing confirmation for a joh email notification request")]
        public void GivenIHaveAHearingConfirmationForAJohEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationJoh;
            var parameters = InitGenericConfirmationParams();
            parameters.Add("judicial office holder", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a hearing confirmation for an ejud joh email notification request")]
        public void GivenIHaveAHearingConfirmationForAnEJudJohEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationEJudJoh;
            var parameters = InitGenericConfirmationParams();
            parameters.Add("judicial office holder", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a hearing confirmation for a LIP email notification request")]
        public void GivenIHaveAHearingConfirmationForALipEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationLip;
            var parameters = InitGenericConfirmationParams();
            parameters.Add("name", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a telephone hearing confirmation for an email notification request")]
        public void GivenIHaveATelephoneHearingConfirmationForAnEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.TelephoneHearingConfirmation;
            var parameters = InitGenericConfirmationParams();
            parameters.Add("name", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a hearing confirmation for a representative email notification request")]
        public void GivenIHaveAHearingConfirmationForARepresentativeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationRepresentative;
            var parameters = InitGenericConfirmationParams();
            parameters.Add("solicitor name", $"{Faker.Name.FullName()}");
            parameters.Add("client name", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        private Dictionary<string, string> InitGenericConfirmationParams()
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
