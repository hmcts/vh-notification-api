using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract;
using NotificationApi.Contract.Responses;
using TechTalk.SpecFlow;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class GetTemplateSteps
    {
        private readonly AcTestContext _context;
        private NotificationType _notificationType;

        public GetTemplateSteps(AcTestContext context)
        {
            _context = context;
        }
        
        [Given(@"I notification type (.*)")]
        public void GivenIHaveAGetTemplateRequest(NotificationType notificationType)
        {
            _notificationType = notificationType;
        }
        
        [When(@"I send a get template by notification type request")]
        public async Task WhenIHaveAGetTemplateRequestRequest()
        {
            await _context.ExecuteApiRequest(() =>
                _context.ApiClient.GetTemplateByNotificationTypeAsync(_notificationType));
        }
        
        [Then(@"a template should return")]
        public void ThenTheTemplateShouldReturn()
        {
            _context.ApiClientResponse.Should().BeOfType<NotificationTemplateResponse>();
            var model = (NotificationTemplateResponse)_context.ApiClientResponse;
            model.Should().NotBeNull();
            model.NotificationType.Should().Be(_notificationType);
            model.Parameters.Should().NotBeNullOrWhiteSpace();
            model.NotifyTemplateId.Should().NotBeEmpty();
            model.Id.Should().BePositive();
        }
        
        [Then(@"should exist in GovUK notify")]
        public async Task ThenAndShouldExistInGovUkNotify()
        {
            var model = (NotificationTemplateResponse)_context.ApiClientResponse;
            var template = await _context.NotifyClient.GetTemplateByIdAsync(model.NotifyTemplateId.ToString());
            template.Should().NotBeNull();
        }
    }
}
