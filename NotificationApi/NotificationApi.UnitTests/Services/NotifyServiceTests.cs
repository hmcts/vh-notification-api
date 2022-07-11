using AdminWebsite.Services;
using Autofac.Extras.Moq;
using FluentAssertions;
using Moq;
using NotificationApi.DAL.Commands;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using NotificationApi.Services;
using Notify.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationApi.UnitTests.Services
{
    public class NotifyServiceTests
    {
        private Template _template;
        private CreateEmailNotificationCommand _createEmailNotificationCommand;
        private AutoMock _mocker;        
        private EmailResponseContent _emailResponseContent;
        private EmailNotificationResponse _expectedEmailNotificationResponse;
        private Dictionary<string, dynamic> _parameters;     
        private CreateNotificationService _createNotificationService;

        [SetUp]
        public void Setup()
        {
            _mocker = AutoMock.GetLoose();          
            _createNotificationService = _mocker.Create<CreateNotificationService>();             
            _emailResponseContent = new EmailResponseContent()
            {
                body = "Email reponse Body"
            };
            
            _template = new Template(Guid.NewGuid(), NotificationType.JudgeDemoOrTest, MessageType.Email, "param1, param2");
            _createEmailNotificationCommand = new CreateEmailNotificationCommand(NotificationType.JudgeDemoOrTest, 
                "testemail@gmail.com", Guid.NewGuid(), Guid.NewGuid(), String.Empty);
            _parameters = new Dictionary<string, dynamic>
            {
                { "Test param1Key", "Test param1Value" }
            };
            _emailResponseContent = new EmailResponseContent()
            {
                body = "Email reponse Body"
            };
            _expectedEmailNotificationResponse = new EmailNotificationResponse()
            {
                id = Guid.NewGuid().ToString(),
                reference = "reference",
                uri = "uri",
                content = _emailResponseContent,
                template = null
            };

            _mocker.Mock<IPollyRetryService>().Setup(x => x.WaitAndRetryAsync<Exception, EmailNotificationResponse>
               (
                   It.IsAny<int>(), It.IsAny<Func<int, TimeSpan>>(), It.IsAny<Action<int>>(), It.IsAny<Func<EmailNotificationResponse, bool>>(), It.IsAny<Func<Task<EmailNotificationResponse>>>()
               ))
               .Callback(async (int retries, Func<int, TimeSpan> sleepDuration, Action<int> retryAction, Func<EmailNotificationResponse, bool> handleResultCondition, Func<Task<EmailNotificationResponse>> executeFunction) =>
               {
                   sleepDuration(1);
                   retryAction(7);
                   handleResultCondition(_expectedEmailNotificationResponse);
                   await executeFunction();
               })
               .ReturnsAsync(_expectedEmailNotificationResponse);
        }

        [Test]
        public async Task Should_verify_send_email_async_is_called_once()
        {
            await _createNotificationService.SendEmailAsyncRetry(_createEmailNotificationCommand.ContactEmail, _template.NotifyTemplateId.ToString(), _parameters, null);

            _mocker.Mock<IPollyRetryService>()
            .Verify(
                x => x.WaitAndRetryAsync<Exception, EmailNotificationResponse>
            (
              It.IsAny<int>(), It.IsAny<Func<int, TimeSpan>>(), It.IsAny<Action<int>>(), It.IsAny<Func<EmailNotificationResponse, bool>>(), It.IsAny<Func<Task<EmailNotificationResponse>>>()
            ), Times.Once);
        }

        [Test]
        public async Task Should_return_response_from_send_email()
        {          
            var result = await _createNotificationService.SendEmailAsyncRetry(_createEmailNotificationCommand.ContactEmail, _template.NotifyTemplateId.ToString(), _parameters, null);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(_expectedEmailNotificationResponse);
        }
    }
}
