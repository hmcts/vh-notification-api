using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Notify.Interfaces;
using Notify.Models;
using Notify.Models.Responses;

namespace NotificationApi.IntegrationTests.Stubs
{
    public class AsyncNotificationClientStub : IAsyncNotificationClient
    {
        public Task<string> GET(string url)
        {
            throw new NotImplementedException();
        }

        public Task<string> POST(string url, string json)
        {
            throw new NotImplementedException();
        }

        public Task<string> MakeRequest(string url, HttpMethod method, HttpContent content = null)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string> ExtractServiceIdAndApiKey(string fromApiKey)
        {
            throw new NotImplementedException();
        }

        public Uri ValidateBaseUri(string baseUrl)
        {
            throw new NotImplementedException();
        }

        public string GetUserAgent()
        {
            throw new NotImplementedException();
        }

        public Task<TemplatePreviewResponse> GenerateTemplatePreviewAsync(string templateId, Dictionary<string, dynamic> personalisation = null)
        {
            throw new NotImplementedException();
        }

        public Task<TemplateList> GetAllTemplatesAsync(string templateType = "")
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetNotificationByIdAsync(string notificationId)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationList> GetNotificationsAsync(string templateType = "", string status = "", string reference = "", string olderThanId = "",
            bool includeSpreadsheetUploads = false)
        {
            throw new NotImplementedException();
        }

        public Task<ReceivedTextListResponse> GetReceivedTextsAsync(string olderThanId = "")
        {
            throw new NotImplementedException();
        }

        public Task<TemplateResponse> GetTemplateByIdAsync(string templateId)
        {
            throw new NotImplementedException();
        }

        public Task<TemplateResponse> GetTemplateByIdAndVersionAsync(string templateId, int version = 0)
        {
            throw new NotImplementedException();
        }

        public Task<SmsNotificationResponse> SendSmsAsync(string mobileNumber, string templateId, Dictionary<string, dynamic> personalisation = null,
            string clientReference = null, string smsSenderId = null)
        {
            throw new NotImplementedException();
        }

        public Task<EmailNotificationResponse> SendEmailAsync(string emailAddress, string templateId, Dictionary<string, dynamic> personalisation = null,
            string clientReference = null, string emailReplyToId = null)
        {
            var id = Guid.NewGuid().ToString();
            var response = new EmailNotificationResponse()
            {
                id = id,
                reference = clientReference,
                content = new EmailResponseContent()
                    {fromEmail = "auto@test.com", body = "random content", subject = "Stub Message"},
                uri = $"https://api.notifications.service.gov.uk/v2/notifications/{id}"
            };
            return Task.FromResult(response);
        }

        public Task<LetterNotificationResponse> SendLetterAsync(string templateId, Dictionary<string, dynamic> personalisation, string clientReference = null)
        {
            throw new NotImplementedException();
        }

        public Task<LetterNotificationResponse> SendPrecompiledLetterAsync(string clientReference, byte[] pdfContents, string postage)
        {
            throw new NotImplementedException();
        }
    }
}
