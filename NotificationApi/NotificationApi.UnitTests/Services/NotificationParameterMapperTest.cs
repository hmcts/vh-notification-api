using System;
using System.Collections.Generic;
using FluentAssertions;
using NotificationApi.Common;
using NotificationApi.Contract.Requests;
using NotificationApi.Extensions;
using NotificationApi.Services;
using NUnit.Framework;

namespace NotificationApi.UnitTests.Services
{
    [TestFixture]
    public class NotificationParameterMapperTest
    {
        [Test]
        public void MapToV1AccountCreated_ShouldMapCorrectly()
        {
            var request = new SignInDetailsEmailRequest
            {
                Name = "John Doe",
                Username = "johndoe",
                Password = "password123"
            };

            var result = NotificationParameterMapper.MapToV1AccountCreated(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(3);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.RandomPassword);
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.RandomPassword].Should().Be("password123");
        }

        [Test]
        public void MapToPasswordReset_ShouldMapCorrectly()
        {
            var request = new PasswordResetEmailRequest
            {
                Name = "John Doe",
                Password = "newpassword123"
            };

            var result = NotificationParameterMapper.MapToPasswordReset(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(2);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.Password);
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.Password].Should().Be("newpassword123");
        }

        [Test]
        public void MapToWelcomeEmail_ShouldMapCorrectly()
        {
            var request = new NewUserWelcomeEmailRequest
            {
                Name = "John Doe",
                CaseName = "Case A",
                CaseNumber = "12345"
            };

            var result = NotificationParameterMapper.MapToWelcomeEmail(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(3);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
        }

        [Test]
        public void MapToSingleDayConfirmationNewUser_ShouldMapCorrectly()
        {
            var request = new NewUserSingleDayHearingConfirmationRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RandomPassword = "password123"
            };

            var result = NotificationParameterMapper.MapToSingleDayConfirmationNewUser(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(9);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.RandomPassword);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.RandomPassword].Should().Be("password123");
        }

        [Test]
        public void MapToMultiDayConfirmationNewUser_ShouldMapCorrectly()
        {
            var request = new NewUserMultiDayHearingConfirmationRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RandomPassword = "password123",
                TotalDays = 3
            };

            var result = NotificationParameterMapper.MapToMultiDayConfirmationNewUser(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(10);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.RandomPassword);
            result.Should().ContainKey(NotifyParams.TotalDays);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.RandomPassword].Should().Be("password123");
            result[NotifyParams.TotalDays].Should().Be("3");
        }

    }
}