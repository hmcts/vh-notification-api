using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.Contract.Responses;
using NotificationApi.Domain.Enums;
using NotificationApi.IntegrationTests.Contexts;
using NotificationApi.IntegrationTests.Helper;
using TechTalk.SpecFlow;
using Testing.Common.Helper;

namespace NotificationApi.IntegrationTests.Steps
{
    [Binding]
    public class GetTemplateSteps : BaseSteps
    {
        private readonly IntTestContext _context;

        public GetTemplateSteps(IntTestContext context)
        {
            _context = context;
        }

        [Given(@"I have a request to get a template by notification type (.*)")]
        public void GivenIHaveARequestToGetATemplateByNotificationType(NotificationType notificationType)
        {
            _context.Uri = ApiUriFactory.NotificationEndpoints.GetTemplateByNotificationType((int) notificationType);
            _context.HttpMethod = HttpMethod.Get;
        }

        [Then(@"the response should contain a template for notification type (.*)")]
        public async Task ThenTheResponseShouldContainATemplate(NotificationType notificationType)
        {
            var templateResponse = await ApiClientResponse.GetResponses<NotificationTemplateResponse>(_context.Response.Content);
            templateResponse.Id.Should().BePositive();
            templateResponse.Parameters.Should().NotBeNullOrWhiteSpace();
            templateResponse.NotificationType.Should().Be((int) notificationType);
            templateResponse.NotifyemplateId.Should().NotBeEmpty();
        }
    }
}
