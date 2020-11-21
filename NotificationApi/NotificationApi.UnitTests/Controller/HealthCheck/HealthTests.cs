using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Contract.Responses;
using NotificationApi.Controllers;
using NUnit.Framework;

namespace NotificationApi.UnitTests.Controller.HealthCheck
{
    public class HealthTests
    {
        private HealthCheckController _controller;
        
        [SetUp]
        public void Setup()
        {
            _controller = new HealthCheckController();
        }
        
        [Test]
        public void Should_return_the_user_api_health()
        {
            var result = _controller.HealthAsync();
            result.Should().BeOfType<OkObjectResult>();
            var typedResult = (OkObjectResult) result;
            var response = (HealthResponse) typedResult.Value;
            response.Version.FileVersion.Should().NotBeNullOrWhiteSpace();
            response.Version.InformationVersion.Should().NotBeNullOrWhiteSpace();
        }
    }
}
