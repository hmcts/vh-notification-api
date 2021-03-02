using Autofac.Extras.Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotificationApi.Common;
using NotificationApi.Contract;
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
    public class GetNotificationByHearingAndParticipantTests
    {
        private AutoMock _mocker;
        private NotificationController _sut;
        private NotificationType notificationType;
        private Guid participantId;
        private Guid hearingId;
        private EmailNotification emailNotification;

        [SetUp]
        public void Setup()
        {
            _mocker = AutoMock.GetLoose();
            _sut = _mocker.Create<NotificationController>();
            notificationType = NotificationType.CreateIndividual;
            participantId = Guid.NewGuid();
            hearingId = Guid.NewGuid();
            emailNotification = new EmailNotification(Guid.NewGuid(), NotificationApi.Domain.Enums.NotificationType.CreateIndividual, "test@hmcts.net", participantId, hearingId);
        }

        [Test]
        public async Task Should_get_notification_by_participant_and_hearing()
        {
            //arrange
            _mocker.Mock<IQueryHandler>()
                   .Setup(x => x.Handle<GetNotificationByParticipantAndHearingQuery, EmailNotification>(It.IsAny<GetNotificationByParticipantAndHearingQuery>()))
                   .ReturnsAsync(emailNotification);

            // Act
            var result = await _sut.GetNotificationByHearingAndParticipantAsync(notificationType, participantId.ToString(), hearingId.ToString());

            // Assert
            _mocker.Mock<IQueryHandler>()
                   .Verify(x => x.Handle<GetNotificationByParticipantAndHearingQuery, EmailNotification>(It.IsAny<GetNotificationByParticipantAndHearingQuery>()), Times.Once);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            okResult.Value.Should().BeOfType<NotificationResponse>();
            var notificationResponse = okResult.Value as NotificationResponse;
            notificationResponse.Id.Should().Be(emailNotification.Id);
        }

        [Test]
        public async Task Should_throw_exception_without_notification_by_participant_and_hearing()
        {
            //arrange 
            _mocker.Mock<IQueryHandler>()
                 .Setup(x => x.Handle<GetNotificationByParticipantAndHearingQuery, EmailNotification>(It.IsAny<GetNotificationByParticipantAndHearingQuery>()))
                 .ReturnsAsync((EmailNotification)null);
            Func<Task> result = async () => await _sut.GetNotificationByHearingAndParticipantAsync(notificationType, hearingId.ToString(), participantId.ToString());

            // Act
            result.Should().Throw<BadRequestException>().WithMessage($"notificationType: {notificationType} does not exists for hearingId: {hearingId}  and participantId: {participantId}");
        }

    }
}
