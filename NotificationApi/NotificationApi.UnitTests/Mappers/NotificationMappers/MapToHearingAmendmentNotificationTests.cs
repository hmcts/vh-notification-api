using System;
using System.Collections.Generic;
using FluentAssertions;
using NotificationApi.Common;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;
using NotificationApi.Extensions;
using NotificationApi.Services;
using NUnit.Framework;

namespace BookingQueueSubscriber.UnitTests.Mappers.NotificationMappers
{
    public class MapToHearingAmendmentNotificationTests
    {

        [Test]
        public void should_map_to_amendment_notification()
        {
            //Arrange
            const NotificationType expectedNotificationType = NotificationType.HearingAmendmentEJudJoh;
            var oldDate = new DateTime(2020, 2, 10, 11, 30, 0, DateTimeKind.Utc);
            var newDate = new DateTime(2020, 10, 12, 13, 10, 0, DateTimeKind.Utc);

            var request = new HearingAmendmentRequest
            {
                HearingId = new Guid("73f2053e-74f1-4d6c-b817-246f4b22e666"),
                ContactEmail = "Automation_226153990@hmcts.net",
                ParticipantId = new Guid("73f2053e-74f1-4d6c-b817-246f4b22e665"),
                CaseName = "Case name",
                PreviousScheduledDateTime = oldDate,
                NewScheduledDateTime = newDate,
                RoleName = "Individual",
                CaseNumber = "Original Hearing",
                Name = $"Automation_FirstName Automation_LastName",
                DisplayName = "Automation_FirstName Automation_LastName",
                Representee = "",
                Username = "Automation_338564597@hmcts.net"
            };

            var expectedParameters = GetExpectedParameters(request);
            
            //Act
            var result = NotificationParameterMapper.MapToHearingAmendment(request);

            //Assert
            result.Should().NotBeNull();
            result[NotifyParams.CaseName].Should().Be(expectedParameters[NotifyParams.CaseName]);
            result[NotifyParams.UserName].Should().Be(expectedParameters[NotifyParams.UserName]);
            result[NotifyParams.CaseNumber].Should().Be(expectedParameters[NotifyParams.CaseNumber]);
            result[NotifyParams.OldTime].Should().Be(expectedParameters[NotifyParams.OldTime]);
            result[NotifyParams.NewTime].Should().Be(expectedParameters[NotifyParams.NewTime]);
            result[NotifyParams.OldDayMonthYear].Should().Be(expectedParameters[NotifyParams.OldDayMonthYear]);
            result[NotifyParams.NewDayMonthYear].Should().Be(expectedParameters[NotifyParams.NewDayMonthYear]);
            result[NotifyParams.Name].Should().Be(expectedParameters[NotifyParams.Name]);
        }
        
        private static Dictionary<string, string> GetExpectedParameters(HearingAmendmentRequest request)
        {
            return new Dictionary<string, string>
            {
                {NotifyParams.CaseName, request.CaseName},
                {NotifyParams.UserName, request.Username.ToLower()},
                {NotifyParams.CaseNumber, request.CaseNumber},
                {NotifyParams.OldTime, request.PreviousScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.NewTime, request.NewScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.OldDayMonthYear, request.PreviousScheduledDateTime.ToEmailDateGbLocale()},
                {NotifyParams.NewDayMonthYear, request.NewScheduledDateTime.ToEmailDateGbLocale()},
                {NotifyParams.Name, request.Name}
            };
        }
    }
}
