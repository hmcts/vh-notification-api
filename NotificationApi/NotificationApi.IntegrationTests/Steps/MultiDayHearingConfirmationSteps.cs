using System.Collections.Generic;
using NotificationApi.Contract;
using NotificationApi.IntegrationTests.Contexts;
using TechTalk.SpecFlow;
using Testing.Common.Extensions;

namespace NotificationApi.IntegrationTests.Steps
{
    [Binding]
    public class MultiDayHearingConfirmationSteps : BaseSteps
    {
        private readonly IntTestContext _context;

        public MultiDayHearingConfirmationSteps(IntTestContext context)
        {
            _context = context;
        }
        
        [Given(@"I have a multi-day hearing confirmation for a judge email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForAJudgeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationJudgeMultiDay;
            var parameters = InitGenericConfirmationMultiParams();
            parameters.Add("judge", $"{Faker.Name.FullName()}");
            parameters.Add("courtroom account username", Faker.Internet.Email());
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a multi-day hearing confirmation for a staffmember email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForAStaffMemberEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationStaffMemberMultiDay;
            var parameters = InitGenericConfirmationMultiParams();
            parameters.Add("staff member", $"{Faker.Name.Last()}");
            parameters.Add("username", Faker.Internet.Email());
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a multi-day hearing confirmation for an ejud judge email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForAnEjudJudgeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationEJudJudgeMultiDay;
            var parameters = InitGenericConfirmationMultiParams();
            parameters.Add("judge", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a multi-day hearing confirmation for a joh email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForAJohEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationJohMultiDay;
            var parameters = InitGenericConfirmationMultiParams();
            parameters.Add("judicial office holder", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a multi-day hearing confirmation for an ejud joh email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForAnEJudJohEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationEJudJohMultiDay;
            var parameters = InitGenericConfirmationMultiParams();
            parameters.Add("judicial office holder", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a multi-day hearing confirmation for a LIP email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForALipEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationLipMultiDay;
            var parameters = InitGenericConfirmationMultiParams();
            parameters.Add("name", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a multi-day telephone hearing confirmation for an email notification request")]
        public void GivenIHaveAMulti_DayTelephoneHearingConfirmationForAnEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationLipMultiDay;
            var parameters = InitGenericConfirmationMultiParams();
            parameters.Add("name", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        [Given(@"I have a multi-day hearing confirmation for a representative email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForARepresentativeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationRepresentativeMultiDay;
            var parameters = InitGenericConfirmationMultiParams();
            parameters.Add("solicitor name", $"{Faker.Name.FullName()}");
            parameters.Add("client name", $"{Faker.Name.FullName()}");
            var request = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
            
            InitCreateNotificationRequest(request, _context);
        }
        
        private Dictionary<string, string> InitGenericConfirmationMultiParams()
        {
            return new Dictionary<string, string>
            {
                {"case number", "UFGFUD/1344"},
                {"case name", "Random Int Test"},
                {"time", "1:10 PM"},
                {"Start Day Month Year", "12 October 2020"},
                {"number of days", "4"}
            };
        }
    }
}
