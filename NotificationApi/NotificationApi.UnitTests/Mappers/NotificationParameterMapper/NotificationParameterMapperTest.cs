using System;
using System.Collections.Generic;
using FluentAssertions;
using NotificationApi.Common;
using NotificationApi.Contract.Requests;
using NotificationApi.Extensions;
using NUnit.Framework;

namespace NotificationApi.UnitTests.Mappers.NotificationParameterMapper
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

            var result = NotificationApi.Services.NotificationParameterMapper.MapToV1AccountCreated(request);

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

            var result = NotificationApi.Services.NotificationParameterMapper.MapToPasswordReset(request);

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

            var result = NotificationApi.Services.NotificationParameterMapper.MapToWelcomeEmail(request);

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

            var result = NotificationApi.Services.NotificationParameterMapper.MapToSingleDayConfirmationNewUser(request);

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

            var result = NotificationApi.Services.NotificationParameterMapper.MapToMultiDayConfirmationNewUser(request);

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

        [Test]
        public void MapToSingleDayConfirmationExistingUser_ShouldMapCorrectly_ForJudge()
        {
            var request = new ExistingUserSingleDayHearingConfirmationRequest
            {
                Name = "John Doe",
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Username = "johndoe",
                RoleName = RoleNames.Judge,
                DisplayName = "Judge John Doe"
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToSingleDayConfirmationExistingUser(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(10);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result.Should().ContainKey(NotifyParams.Judge);
            result.Should().ContainKey(NotifyParams.CourtroomAccountUserName);
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Judge].Should().Be("Judge John Doe");
            result[NotifyParams.CourtroomAccountUserName].Should().Be("johndoe");
        }

        [Test]
        public void MapToSingleDayConfirmationExistingUser_ShouldMapCorrectly_ForJudicialOfficeHolder()
        {
            var request = new ExistingUserSingleDayHearingConfirmationRequest
            {
                Name = "John Doe",
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Username = "johndoe",
                RoleName = RoleNames.JudicialOfficeHolder
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToSingleDayConfirmationExistingUser(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(9);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result.Should().ContainKey(NotifyParams.JudicialOfficeHolder);
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.JudicialOfficeHolder].Should().Be("John Doe");
        }

        [Test]
        public void MapToSingleDayConfirmationExistingUser_ShouldMapCorrectly_ForRepresentative()
        {
            var request = new ExistingUserSingleDayHearingConfirmationRequest
            {
                Name = "John Doe",
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Username = "johndoe",
                RoleName = RoleNames.Representative,
                Representee = "Client A"
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToSingleDayConfirmationExistingUser(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(10);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result.Should().ContainKey(NotifyParams.ClientName);
            result.Should().ContainKey(NotifyParams.SolicitorName);
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.ClientName].Should().Be("Client A");
            result[NotifyParams.SolicitorName].Should().Be("John Doe");
        }

        [Test]
        public void MapToSingleDayConfirmationExistingUser_ShouldMapCorrectly_ForIndividual()
        {
            var request = new ExistingUserSingleDayHearingConfirmationRequest
            {
                Name = "John Doe",
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Username = "johndoe",
                RoleName = RoleNames.Individual
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToSingleDayConfirmationExistingUser(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(8);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
        }





        [Test]
        public void MapToMultiDayConfirmationForExistingUser_ShouldMapCorrectly_ForJudge()
        {
            var request = new ExistingUserMultiDayHearingConfirmationRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RoleName = RoleNames.Judge,
                DisplayName = "Judge John Doe",
                TotalDays = 3
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToMultiDayConfirmationForExistingUser(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(12);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.StartDayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.TotalDays);
            result.Should().ContainKey(NotifyParams.Judge);
            result.Should().ContainKey(NotifyParams.CourtroomAccountUserName);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.StartDayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.TotalDays].Should().Be("3");
            result[NotifyParams.Judge].Should().Be("Judge John Doe");
            result[NotifyParams.CourtroomAccountUserName].Should().Be("johndoe");
        }

        [Test]
        public void MapToMultiDayConfirmationForExistingUser_ShouldMapCorrectly_ForJudicialOfficeHolder()
        {
            var request = new ExistingUserMultiDayHearingConfirmationRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RoleName = RoleNames.JudicialOfficeHolder,
                TotalDays = 3
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToMultiDayConfirmationForExistingUser(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(11);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.StartDayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.TotalDays);
            result.Should().ContainKey(NotifyParams.JudicialOfficeHolder);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.StartDayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.TotalDays].Should().Be("3");
            result[NotifyParams.JudicialOfficeHolder].Should().Be("John Doe");
        }

        [Test]
        public void MapToMultiDayConfirmationForExistingUser_ShouldMapCorrectly_ForRepresentative()
        {
            var request = new ExistingUserMultiDayHearingConfirmationRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RoleName = RoleNames.Representative,
                Representee = "Client A",
                TotalDays = 3
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToMultiDayConfirmationForExistingUser(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(12);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.StartDayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.TotalDays);
            result.Should().ContainKey(NotifyParams.ClientName);
            result.Should().ContainKey(NotifyParams.SolicitorName);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.StartDayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.TotalDays].Should().Be("3");
            result[NotifyParams.ClientName].Should().Be("Client A");
            result[NotifyParams.SolicitorName].Should().Be("John Doe");
        }

        [Test]
        public void MapToMultiDayConfirmationForExistingUser_ShouldMapCorrectly_ForIndividual()
        {
            var request = new ExistingUserMultiDayHearingConfirmationRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RoleName = RoleNames.Individual,
                TotalDays = 3
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToMultiDayConfirmationForExistingUser(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(10);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.StartDayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result.Should().ContainKey(NotifyParams.TotalDays);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.StartDayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.TotalDays].Should().Be("3");
        }


        [Test]
        public void MapToSingleDayReminder_ShouldMapCorrectly_ForJudicialOfficeHolder()
        {
            var request = new SingleDayHearingReminderRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RoleName = RoleNames.JudicialOfficeHolder
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToSingleDayReminder(request);

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
            result.Should().ContainKey(NotifyParams.JudicialOfficeHolder);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.JudicialOfficeHolder].Should().Be("John Doe");
        }

        [Test]
        public void MapToSingleDayReminder_ShouldMapCorrectly_ForRepresentative()
        {
            var request = new SingleDayHearingReminderRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RoleName = RoleNames.Representative,
                Representee = "Client A"
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToSingleDayReminder(request);

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
            result.Should().ContainKey(NotifyParams.ClientName);
            result.Should().ContainKey(NotifyParams.SolicitorName);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.ClientName].Should().Be("Client A");
            result[NotifyParams.SolicitorName].Should().Be("John Doe");
        }

        [Test]
        public void MapToSingleDayReminder_ShouldMapCorrectly_ForIndividual()
        {
            var request = new SingleDayHearingReminderRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RoleName = RoleNames.Individual
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToSingleDayReminder(request);

            result.Should().NotBeNull();
            result.Should().BeOfType<Dictionary<string, string>>();
            result.Should().HaveCount(8);
            result.Should().ContainKey(NotifyParams.CaseName);
            result.Should().ContainKey(NotifyParams.CaseNumber);
            result.Should().ContainKey(NotifyParams.Time);
            result.Should().ContainKey(NotifyParams.DayMonthYear);
            result.Should().ContainKey(NotifyParams.DayMonthYearCy);
            result.Should().ContainKey(NotifyParams.Name);
            result.Should().ContainKey(NotifyParams.StartTime);
            result.Should().ContainKey(NotifyParams.UserName);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
        }

        [Test]
        public void MapToMultiDayReminder_ShouldMapCorrectly_ForJudicialOfficeHolder()
        {
            var request = new MultiDayHearingReminderRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RoleName = RoleNames.JudicialOfficeHolder,
                TotalDays = 3
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToMultiDayReminder(request);

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
            result.Should().ContainKey(NotifyParams.TotalDays);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.TotalDays].Should().Be("3");
        }

        [Test]
        public void MapToMultiDayReminder_ShouldMapCorrectly_ForRepresentative()
        {
            var request = new MultiDayHearingReminderRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RoleName = RoleNames.Representative,
                TotalDays = 3
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToMultiDayReminder(request);

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
            result.Should().ContainKey(NotifyParams.TotalDays);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.TotalDays].Should().Be("3");
        }

        [Test]
        public void MapToMultiDayReminder_ShouldMapCorrectly_ForIndividual()
        {
            var request = new MultiDayHearingReminderRequest
            {
                CaseName = "Case A",
                CaseNumber = "12345",
                ScheduledDateTime = DateTime.Now.ToUniversalTime(),
                Name = "John Doe",
                Username = "johndoe",
                RoleName = RoleNames.Individual,
                TotalDays = 3
            };

            var result = NotificationApi.Services.NotificationParameterMapper.MapToMultiDayReminder(request);

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
            result.Should().ContainKey(NotifyParams.TotalDays);
            result[NotifyParams.CaseName].Should().Be("Case A");
            result[NotifyParams.CaseNumber].Should().Be("12345");
            result[NotifyParams.Time].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.DayMonthYear].Should().Be(request.ScheduledDateTime.ToEmailDateGbLocale());
            result[NotifyParams.DayMonthYearCy].Should().Be(request.ScheduledDateTime.ToEmailDateCyLocale());
            result[NotifyParams.Name].Should().Be("John Doe");
            result[NotifyParams.StartTime].Should().Be(request.ScheduledDateTime.ToEmailTimeGbLocale());
            result[NotifyParams.UserName].Should().Be("johndoe");
            result[NotifyParams.TotalDays].Should().Be("3");
        }
    }
}
