using System.Net;
using FluentAssertions;
using NotificationApi.AcceptanceTests.Contexts;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public sealed class CommonSteps
    {
        private readonly AcTestContext _context;

        public CommonSteps(AcTestContext acTestContext)
        {
            _context = acTestContext;
        }

        [When(@"I send the request to the endpoint")]
        [When(@"I resend the request to the endpoint")]
        public void WhenISendTheRequestToTheEndpoint()
        {
            _context.Response = _context.Client().Execute(_context.Request);
        }

        [Then(@"the response should have the status (.*) and success status (.*)")]
        public void ThenTheResponseShouldHaveTheStatusAndSuccessStatus(HttpStatusCode httpStatusCode, bool isSuccess)
        {
            _context.Response.StatusCode.Should().Be(httpStatusCode);
            _context.Response.IsSuccessful.Should().Be(isSuccess);
        }
    }
}
