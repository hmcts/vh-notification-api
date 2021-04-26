using System.Collections.Generic;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract;
using TechTalk.SpecFlow;
using Testing.Common.Extensions;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class MultiDayHearingConfirmationSteps 
    {
        private readonly AcTestContext _context;

        public MultiDayHearingConfirmationSteps(AcTestContext context)
        {
            _context = context;
        }
        
        [Given(@"I have a multi-day hearing confirmation for a judge email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForAJudgeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationJudgeMultiDay;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judge", $"{Faker.Name.FullName()}");
            parameters.Add("courtroom account username", Faker.Internet.Email());
            _context.CreateNotificationRequest = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }
        
        [Given(@"I have a multi-day hearing confirmation for an ejud judge email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForAnEjudJudgeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationEJudJudgeMultiDay;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judge", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }
        
        [Given(@"I have a multi-day hearing confirmation for a joh email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForAJohEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationJohMultiDay;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("judicial office holder", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }
        
        [Given(@"I have a multi-day hearing confirmation for a LIP email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForALipEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationLipMultiDay;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("name", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }
        
        [Given(@"I have a multi-day hearing confirmation for a representative email notification request")]
        public void GivenIHaveAMulti_DayHearingConfirmationForARepresentativeEmailNotificationRequest()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.HearingConfirmationRepresentativeMultiDay;
            var parameters = InitGenericAmendmentParams();
            parameters.Add("solicitor name", $"{Faker.Name.FullName()}");
            parameters.Add("client name", $"{Faker.Name.FullName()}");
            _context.CreateNotificationRequest = AddNotificationRequestBuilder.BuildRequest(messageType, templateType, parameters);
        }
        
        private Dictionary<string, string> InitGenericAmendmentParams()
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
