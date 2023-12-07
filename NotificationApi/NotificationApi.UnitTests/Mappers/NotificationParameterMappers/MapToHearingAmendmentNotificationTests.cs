using System;
using System.Collections.Generic;
using FluentAssertions;
using NotificationApi.Common;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;
using NotificationApi.Extensions;
using NotificationApi.Services;
using NUnit.Framework;

namespace NotificationApi.UnitTests.Mappers.NotificationParameterMappers
{
    public class MapToHearingAmendmentNotificationTests
    {

        [Test]
        public void should_map_to_amendment_notification_for_individual()
        {
            //Arrange
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
            
            //Act
            var result = NotificationParameterMapper.MapToHearingAmendment(request);

            //Assert
            result.Should().NotBeNull();
            result[NotifyParams.CaseName].Should().Be("Case name");
            result[NotifyParams.UserName].Should().Be("automation_338564597@hmcts.net");
            result[NotifyParams.CaseNumber].Should().Be("Original Hearing");
            result[NotifyParams.OldTime].Should().Be("11:30 AM");
            result[NotifyParams.NewTime].Should().Be("2:10 PM");
            result[NotifyParams.OldDayMonthYear].Should().Be("10 February 2020");
            result[NotifyParams.NewDayMonthYear].Should().Be("12 October 2020");
            result[NotifyParams.Name].Should().Be("Automation_FirstName Automation_LastName");
        }
    }
}
