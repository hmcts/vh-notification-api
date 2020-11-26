using Autofac.Extras.Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotificationApi.Common;
using NotificationApi.Contract.Responses;
using NotificationApi.Controllers;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.UnitTests.Controller.Notification
{
    public class GetTemplateByNotificationTypeShould
    {
        private AutoMock _mocker;

        private NotificationController _sut;

        [SetUp]
        public void Setup()
        {
            _mocker = AutoMock.GetLoose();
            _sut = _mocker.Create<NotificationController>();
        }

        [Test]
        public async Task Should_get_template_by_notification_type_happy()
        {
            foreach (var notificationType in Enum.GetValues(typeof(NotificationType)).OfType<NotificationType>())
            {
                // Arrange
                var template = new Template(Guid.NewGuid(), notificationType, MessageType.Email, "parameters");
                _mocker.Mock<IQueryHandler>().Setup(x => x.Handle<GetTemplateByNotificationTypeQuery, Template>(It.Is<GetTemplateByNotificationTypeQuery>(y => y.NotificationType == notificationType))).ReturnsAsync(template);

                // Act
                var result = await _sut.GetTemplateByNotificationType((int)notificationType);

                // Assert
                _mocker.Mock<IQueryHandler>().Verify(x => x.Handle<GetTemplateByNotificationTypeQuery, Template>(It.Is<GetTemplateByNotificationTypeQuery>(y => y.NotificationType == notificationType)), Times.Once);

                result.Should().BeOfType<OkObjectResult>();
                var okResult = result as OkObjectResult;

                okResult.Value.Should().BeOfType<NotificationTemplateResponse>();
                var notificationTemplateResponse = okResult.Value as NotificationTemplateResponse;
                notificationTemplateResponse.Id.Should().Be(template.Id);
                notificationTemplateResponse.NotificationType.Should().Be((int)notificationType);
                notificationTemplateResponse.NotifyemplateId.Should().Be(template.NotifyTemplateId);
                notificationTemplateResponse.Parameters.Should().Be(template.Parameters);
            }
        }

        [Test]
        public void Should_get_template_by_notification_type_invalid_notificationType()
        {
            // Arrange

            // Act / Assert
            var exception = Assert.ThrowsAsync<BadRequestException>(() => _sut.GetTemplateByNotificationType(100000));
            _mocker.Mock<IQueryHandler>().Verify(x => x.Handle<GetTemplateByNotificationTypeQuery, Template>(It.IsAny<GetTemplateByNotificationTypeQuery>()), Times.Once);
            exception.Message.Should().Be($"Invalid notificationType: 100000");
        }
    }
}
