using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract.Responses;
using TechTalk.SpecFlow;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class GetPasswordNotificationSteps
    {
        private readonly AcTestContext _context;

        public GetPasswordNotificationSteps(AcTestContext context)
        {
            _context = context;
        }

        [When(@"I send a request to get password notification by email")]
        public async Task WhenIHaveAGetTemplateRequestRequest()
        {
            await _context.ExecuteApiRequest(() =>
                _context.ApiClient.GetPasswordNotificationByEmailAsync("email@hmcts.net"));
        }

        [Then(@"the result should have a valid response")]
        public void ThenTheResultShouldHaveAValidResponse()
        {
            _context.ApiClientResponse.Should().BeOfType<List<NotificationResponse>>();
            var model = (List<NotificationResponse>)_context.ApiClientResponse;
            model.Should().NotBeNull();
            model.Count.Should().BeGreaterThan(0);
        }
    }
}
