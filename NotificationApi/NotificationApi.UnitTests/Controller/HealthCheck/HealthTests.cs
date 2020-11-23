using System;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotificationApi.Contract.Responses;
using NotificationApi.Controllers;
using NotificationApi.DAL.Models;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NUnit.Framework;

namespace NotificationApi.UnitTests.Controller.HealthCheck
{
    public class HealthTests
    {
        private AutoMock _mocker;
        private Mock<IQueryHandler> _queryHandlerMock;
        private HealthCheckController _controller;
        
        [SetUp]
        public void Setup()
        {
            _mocker = AutoMock.GetLoose();
            _queryHandlerMock = _mocker.Mock<IQueryHandler>();
            _controller = _mocker.Create<HealthCheckController>();
        }
        
        [Test]
        public async Task Should_return_the_user_api_health()
        {
            var connectResult = new DbHealthCheckResult {CanConnect = true};
            _queryHandlerMock
                .Setup(x => x.Handle<DbHealthCheckQuery, DbHealthCheckResult>(It.IsAny<DbHealthCheckQuery>()))
                .ReturnsAsync(connectResult);
            
            var result = await _controller.HealthAsync();
            result.Should().BeOfType<OkObjectResult>();
            var typedResult = (OkObjectResult) result;
            var response = (HealthResponse) typedResult.Value;
            response.AppVersion.FileVersion.Should().NotBeNullOrWhiteSpace();
            response.AppVersion.InformationVersion.Should().NotBeNullOrWhiteSpace();
            response.DatabaseHealth.Successful.Should().BeTrue();
        }

        [Test]
        public async Task should_return_error_status_code_if_db_connection_fails()
        {
            var connectResult = new DbHealthCheckResult {CanConnect = false};
            _queryHandlerMock
                .Setup(x => x.Handle<DbHealthCheckQuery, DbHealthCheckResult>(It.IsAny<DbHealthCheckQuery>()))
                .ReturnsAsync(connectResult);
            
            var result = await _controller.HealthAsync();
            result.Should().BeOfType<ObjectResult>();
            var typedResult = (ObjectResult) result;
            var response = (HealthResponse) typedResult.Value;
            response.AppVersion.FileVersion.Should().NotBeNullOrWhiteSpace();
            response.AppVersion.InformationVersion.Should().NotBeNullOrWhiteSpace();
            response.DatabaseHealth.Successful.Should().BeFalse();
        }
        
        [Test]
        public async Task should_return_error_status_code_if_db_connection_throws_exception()
        {
            var exception = new AggregateException("database connection failed");
            _queryHandlerMock
                .Setup(x => x.Handle<DbHealthCheckQuery, DbHealthCheckResult>(It.IsAny<DbHealthCheckQuery>()))
                .ThrowsAsync(exception);
            
            var result = await _controller.HealthAsync();
            result.Should().BeOfType<ObjectResult>();
            var typedResult = (ObjectResult) result;
            var response = (HealthResponse) typedResult.Value;
            response.AppVersion.FileVersion.Should().NotBeNullOrWhiteSpace();
            response.AppVersion.InformationVersion.Should().NotBeNullOrWhiteSpace();
            response.DatabaseHealth.Successful.Should().BeFalse();
            response.DatabaseHealth.ErrorMessage.Should().NotBeNullOrWhiteSpace();
        }
    }
}
