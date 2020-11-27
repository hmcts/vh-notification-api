using Autofac.Extras.Moq;
using Microsoft.Extensions.Logging;
using Moq;
using NotificationApi.Middleware.Logging;
using Notify.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationApi.UnitTests.Middleware
{
    public class AsyncNotificationClientLoggingDecoratorShould
    {
        private AutoMock _mocker;

        private AsyncNotificationClientLoggingDecorator _sut;

        [SetUp]
        public void Setup()
        {
            _mocker = AutoMock.GetLoose();
            _sut = _mocker.Create<AsyncNotificationClientLoggingDecorator>();
        }

        [Test]
        public void Should_call_underlying_client_ExtractServiceIdAndApiKey()
        {
            // Arrange
            var fromApiKey = "fromApiKey";

            // Act
            _sut.ExtractServiceIdAndApiKey(fromApiKey);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.ExtractServiceIdAndApiKey(fromApiKey), Times.Once);
        }

        [Test]
        public void Should_call_underlying_client_GetUserAgent()
        {
            // Arrange

            // Act
            _sut.GetUserAgent();

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.GetUserAgent(), Times.Once);
        }

        [Test]
        public void Should_call_underlying_client_ValidateBaseUri()
        {
            // Arrange
            var baseUrl = "baseUrl";

            // Act
            _sut.ValidateBaseUri(baseUrl);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.ValidateBaseUri(baseUrl), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_GenerateTemplatePreviewAsync()
        {
            // Arrange
            var templateId = "templateId";
            var personalisation = new Dictionary<string, dynamic>();

            // Act
            await _sut.GenerateTemplatePreviewAsync(templateId, personalisation);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.GenerateTemplatePreviewAsync(templateId, personalisation), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_GetAllTemplatesAsync()
        {
            // Arrange
            var templateType = "templateType";

            // Act
            await _sut.GetAllTemplatesAsync(templateType);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.GetAllTemplatesAsync(templateType), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_GetNotificationByIdAsync()
        {
            // Arrange
            var notificationId = "notificationId";

            // Act
            await _sut.GetNotificationByIdAsync(notificationId);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.GetNotificationByIdAsync(notificationId), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_GetNotificationsAsync()
        {
            // Arrange
            var templateType = "templateType";
            var status = "status";
            var reference = "reference";
            var olderThanId = "olderThanId";
            var includeSpreadsheetUploads = true;

            // Act
            await _sut.GetNotificationsAsync(templateType, status, reference, olderThanId, includeSpreadsheetUploads);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.GetNotificationsAsync(templateType, status, reference, olderThanId, includeSpreadsheetUploads), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_GetReceivedTextsAsync()
        {
            // Arrange
            var olderThanId = "olderThanId";

            // Act
            await _sut.GetReceivedTextsAsync(olderThanId);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.GetReceivedTextsAsync(olderThanId), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_GetTemplateByIdAsync()
        {
            // Arrange
            var templateId = "templateId";

            // Act
            await _sut.GetTemplateByIdAsync(templateId);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.GetTemplateByIdAsync(templateId), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_MakeRequest()
        {
            // Arrange
            var url = "url";
            var method = HttpMethod.Get;
            var content = new MultipartContent();

            // Act
            await _sut.MakeRequest(url, method, content);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.MakeRequest(url, method, content), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_POST()
        {
            // Arrange
            var url = "url";
            var json = "json";

            // Act
            await _sut.POST(url, json);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.POST(url, json), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_SendEmailAsync()
        {
            // Arrange
            var emailAddress = "emailAddress";
            var templateId = "templateId";
            var personalisation = new Dictionary<string, dynamic>();
            var clientReference = "clientReference";
            var emailReplyToId = "emailReplyToId";

            // Act
            await _sut.SendEmailAsync(emailAddress, templateId, personalisation, clientReference, emailReplyToId);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.SendEmailAsync(emailAddress, templateId, personalisation, clientReference, emailReplyToId), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_SendLetterAsync()
        {
            // Arrange
            var templateId = "templateId";
            var personalisation = new Dictionary<string, dynamic>();
            var clientReference = "clientReference";

            // Act
            await _sut.SendLetterAsync(templateId, personalisation, clientReference);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.SendLetterAsync(templateId, personalisation, clientReference), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_SendPrecompiledLetterAsync()
        {
            // Arrange
            var clientReference = "clientReference";
            var pdfContents = new byte[0];
            var postage = "postage";

            // Act
            await _sut.SendPrecompiledLetterAsync(clientReference, pdfContents, postage);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.SendPrecompiledLetterAsync(clientReference, pdfContents, postage), Times.Once);
        }

        [Test]
        public async Task Should_call_underlying_client_SendSmsAsync()
        {
            // Arrange
            var mobileNumber = "mobileNumber";
            var templateId = "templateId";
            var personalisation = new Dictionary<string, dynamic>();
            var clientReference = "clientReference";
            var smsSenderId = "smsSenderId";

            // Act
            await _sut.SendSmsAsync(mobileNumber, templateId, personalisation, clientReference, smsSenderId);

            // Assert
            _mocker.Mock<IAsyncNotificationClient>().Verify(x => x.SendSmsAsync(mobileNumber, templateId, personalisation, clientReference, smsSenderId), Times.Once);
        }
    }
}
