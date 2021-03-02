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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationApi.UnitTests.Controller.Notification
{
    public class GetNotificationByEmailTests
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
        public async Task Should_get_password_reset_notification_by_email()
        {
                //arrange
                var emailId = "test@hmcts.net";
                var emailNotification = new EmailNotification(Guid.NewGuid(), NotificationApi.Domain.Enums.NotificationType.PasswordReset,"test@hmcts.net", Guid.Empty, Guid.Empty);
                 _mocker.Mock<IQueryHandler>()
                        .Setup(x => x.Handle<GetNotificationByEmailQuery, List<EmailNotification>>(It.IsAny<GetNotificationByEmailQuery>()))
                        .ReturnsAsync(new List<EmailNotification>() { emailNotification });

                // Act
                var result = await _sut.GetPasswordNotificationByEmailAsync(emailId);

                // Assert
                _mocker.Mock<IQueryHandler>().Verify(x => x.Handle<GetNotificationByEmailQuery, List<EmailNotification>>(It.Is<GetNotificationByEmailQuery>(q => q.Email == emailId)), Times.Once);

                result.Should().BeOfType<OkObjectResult>();
                var okResult = result as OkObjectResult;

                okResult.Value.Should().BeOfType<List<NotificationResponse>>();
                var notificationResponse = okResult.Value as List<NotificationResponse>;
                notificationResponse[0].Id.Should().Be(emailNotification.Id);
        }

        [Test]
        public async Task Should_throw_exception_without_password_notification_by_email()
        {
            //arrange  
            var email = "notexists@hmcts.net";
            _mocker.Mock<IQueryHandler>()
                   .Setup(x => x.Handle<GetNotificationByEmailQuery, List<EmailNotification>>(It.IsAny<GetNotificationByEmailQuery>()))
                   .ReturnsAsync((List<EmailNotification>)null);
            Func<Task> result = async () => await _sut.GetPasswordNotificationByEmailAsync(email);

            // Act
            result.Should().Throw<BadRequestException>().WithMessage($"Notification does not exists for email: {email}");
        }
    }
}
