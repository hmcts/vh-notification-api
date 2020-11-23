using System.Threading.Tasks;
using AcceptanceTests.Common.Api.Helpers;
using FluentAssertions;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract.Responses;
using TechTalk.SpecFlow;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class HealthCheckSteps
    {
        private readonly AcTestContext _context;

        public HealthCheckSteps(AcTestContext context)
        {
            _context = context;
        }

        [Given(@"I send a get health request")]
        public async Task GivenIHaveAGetHealthRequest()
        {
            await _context.ExecuteApiRequest(() => _context.ApiClient.CheckServiceHealthAuthAsync());
        }

        [Then(@"the application version should be retrieved")]
        public void ThenTheApplicationVersionShouldBeRetrieved()
        {
            _context.ApiClientResponse.Should().BeOfType<HealthResponse>();
            var model = (HealthResponse)_context.ApiClientResponse;
            model.Should().NotBeNull();
            model.AppVersion.Should().NotBeNull();
            model.AppVersion.FileVersion.Should().NotBeNull();
            model.AppVersion.InformationVersion.Should().NotBeNull();
        }
        
        [Then(@"the database health should be okay")]
        public void ThenTheDatabaseHealthShouldBeRetrieved()
        {
            _context.ApiClientResponse.Should().BeOfType<HealthResponse>();
            var model = (HealthResponse)_context.ApiClientResponse;
            model.Should().NotBeNull();
            model.DatabaseHealth.Should().NotBeNull();
            model.DatabaseHealth.Successful.Should().BeTrue();
        }
    }
}
