using System.Collections.Generic;
using NotificationApi.Contract;
using NotificationApi.IntegrationTests.Contexts;
using TechTalk.SpecFlow;
using Testing.Common.Extensions;

namespace NotificationApi.IntegrationTests.Steps
{
    [Binding]
    public class HearingAmendmentSteps : BaseSteps
    {
        private readonly IntTestContext _context;

        public HearingAmendmentSteps(IntTestContext context)
        {
            _context = context;
        }
        
        [Given(@"I have a hearing amendment for a judge email notification request")]
        public void GivenIHaveAHearingAmendmentForAJudgeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingAmendmentJudge;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judge", $"{Faker.Name.FullName()}");
            parameters.Add("courtroom account username", Faker.Internet.Email());
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }

        [Given(@"I have a hearing amendment for a judge demo or test email notification request")]
        public void GivenIHaveAHearingAmendmentForAJudgeDemoOrTestEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.JudgeDemoOrTest;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("test type", $"{Faker.Name.FullName()}");
            parameters.Add("date", "15 February 2021");
            parameters.Add("time", "12:15pm");
            parameters.Add("Judge", $"{Faker.Name.FullName()}");
            parameters.Add("courtroom account username", Faker.Internet.Email());
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);

            InitCreateNotificationRequest(request, _context);
        }

        [Given(@"I have a hearing amendment for an ejud judge email notification request")]
        public void GivenIHaveAHearingAmendmentForAnEjudJudgeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingAmendmentEJudJudge;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judge", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }

        [Given(@"I have a hearing amendment for an ejud judge demo or test email notification request")]
        public void GivenIHaveAHearingAmendmentForAnEjudJudgeDemoOrTestEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.EJudJudgeDemoOrTest;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("test type", $"{Faker.Name.FullName()}");
            parameters.Add("date", "15 February 2021");
            parameters.Add("time", "12:15pm");
            parameters.Add("Judge", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);

            InitCreateNotificationRequest(request, _context);
        }

        [Given(@"I have a hearing amendment for an ejud joh email notification request")]
        public void GivenIHaveAHearingAmendmentForAnEjudJohEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingAmendmentEJudJoh;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judicial office holder", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        } 
        
        [Given(@"I have a hearing amendment for an ejud joh demo or test email notification request")]
        public void GivenIHaveAHearingAmendmentForAnEjudJohDemoOrTestEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.EJudJohDemoOrTest;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judicial office holder", $"{Faker.Name.FullName()}");
            parameters.Add("test type", $"{Faker.Name.FullName()}");
            parameters.Add("date", "15 February 2021");
            parameters.Add("time", "12:15pm");
            parameters.Add("username", "test USer Name");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a hearing amendment for a joh email notification request")]
        public void GivenIHaveAHearingAmendmentForAJohEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingAmendmentJoh;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judicial office holder", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a hearing amendment for a LIP email notification request")]
        public void GivenIHaveAHearingAmendmentForALipEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingAmendmentLip;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("name", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a hearing amendment for a representative email notification request")]
        public void GivenIHaveAHearingAmendmentForARepresentativeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingAmendmentRepresentative;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("solicitor name", $"{Faker.Name.FullName()}");
            parameters.Add("client name", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }

        [Given(@"I have a hearing amendment for a participant demo or test email notification request")]
        public void GivenIHaveAHearingAmendmentForAParticipantDemoOrTestEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.ParticipantDemoOrTest;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("name", $"{Faker.Name.FullName()}");
            parameters.Add("test type", $"{Faker.Name.FullName()}");
            parameters.Add("date", "15 February 2021");
            parameters.Add("time", "12:15pm");
            parameters.Add("username", "test User Name");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);

            InitCreateNotificationRequest(request, _context);
        }

        private Dictionary<string, string> InitGenericAmendmentParams()
        {
            return new Dictionary<string, string>
            {
                {"case number", "UFGFUD/1344"},
                {"case name", "Random Int Test"},
                {"Old time", "11:30 AM"},
                {"New time", "1:10 PM"},
                {"Old Day Month Year", "10 February 2020"},
                {"New Day Month Year", "12 October 2020"}
            };
        }
    }
}
