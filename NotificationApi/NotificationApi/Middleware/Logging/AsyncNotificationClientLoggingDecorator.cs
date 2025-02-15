using Microsoft.Extensions.Logging;
using Notify.Interfaces;
using Notify.Models;
using Notify.Models.Responses;
using System.Diagnostics;
using System.Net.Http;

namespace NotificationApi.Middleware.Logging
{
    public class AsyncNotificationClientLoggingDecorator(
        IAsyncNotificationClient underlyingNotificationClient,
        ILogger<AsyncNotificationClientLoggingDecorator> logger)
        : IAsyncNotificationClient
    {
        public Tuple<string, string> ExtractServiceIdAndApiKey(string fromApiKey) => underlyingNotificationClient.ExtractServiceIdAndApiKey(fromApiKey);

        public string GetUserAgent() => underlyingNotificationClient.GetUserAgent();

        public Uri ValidateBaseUri(string baseUrl) => underlyingNotificationClient.ValidateBaseUri(baseUrl);

        public Task<TemplatePreviewResponse> GenerateTemplatePreviewAsync(string templateId, Dictionary<string, dynamic> personalisation = null) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(GenerateTemplatePreviewAsync),
            [nameof(templateId)] = templateId,
            [nameof(personalisation)] = personalisation
        }, underlyingNotificationClient.GenerateTemplatePreviewAsync, templateId, personalisation);

        public Task<string> GET(string url) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(GET),
            [nameof(url)] = url
        }, underlyingNotificationClient.GET, url);

        public Task<TemplateList> GetAllTemplatesAsync(string templateType = "") => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(GetAllTemplatesAsync),
            [nameof(templateType)] = templateType
        }, underlyingNotificationClient.GetAllTemplatesAsync, templateType);

        public Task<Notification> GetNotificationByIdAsync(string notificationId) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(GetNotificationByIdAsync),
            [nameof(notificationId)] = notificationId
        }, underlyingNotificationClient.GetNotificationByIdAsync, notificationId);

        public Task<NotificationList> GetNotificationsAsync(string templateType = "", string status = "", string reference = "", string olderThanId = "", bool includeSpreadsheetUploads = false) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(GetNotificationsAsync),
            [nameof(templateType)] = templateType,
            [nameof(status)] = status,
            [nameof(reference)] = reference,
            [nameof(olderThanId)] = olderThanId,
            [nameof(includeSpreadsheetUploads)] = includeSpreadsheetUploads
        }, underlyingNotificationClient.GetNotificationsAsync, templateType, status, reference, olderThanId, includeSpreadsheetUploads);

        public Task<ReceivedTextListResponse> GetReceivedTextsAsync(string olderThanId = "") => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(GetReceivedTextsAsync),
            [nameof(olderThanId)] = olderThanId
        }, underlyingNotificationClient.GetReceivedTextsAsync, olderThanId);

        public Task<TemplateResponse> GetTemplateByIdAndVersionAsync(string templateId, int version = 0) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(GetTemplateByIdAndVersionAsync),
            [nameof(templateId)] = templateId,
            [nameof(version)] = version
        }, underlyingNotificationClient.GetTemplateByIdAndVersionAsync, templateId, version);

        public Task<TemplateResponse> GetTemplateByIdAsync(string templateId) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(GetTemplateByIdAsync),
            [nameof(templateId)] = templateId
        }, underlyingNotificationClient.GetTemplateByIdAsync, templateId);

        public Task<string> MakeRequest(string url, HttpMethod method, HttpContent content = null) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(MakeRequest),
            [nameof(url)] = url,
            [nameof(method)] = method,
            [nameof(content)] = content
        }, underlyingNotificationClient.MakeRequest, url, method, content);

        public Task<string> POST(string url, string json) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(POST),
            [nameof(url)] = url,
            [nameof(json)] = json
        }, underlyingNotificationClient.POST, url, json);

        public Task<EmailNotificationResponse> SendEmailAsync(string emailAddress, string templateId, Dictionary<string, dynamic> personalisation = null, string clientReference = null, string emailReplyToId = null) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(SendEmailAsync),
            [nameof(emailAddress)] = emailAddress,
            [nameof(templateId)] = templateId,
            [nameof(personalisation)] = personalisation,
            [nameof(clientReference)] = clientReference,
            [nameof(emailReplyToId)] = emailReplyToId
        }, underlyingNotificationClient.SendEmailAsync, emailAddress, templateId, personalisation, clientReference, emailReplyToId);

        public Task<LetterNotificationResponse> SendLetterAsync(string templateId, Dictionary<string, dynamic> personalisation, string clientReference = null) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(SendLetterAsync),
            [nameof(templateId)] = templateId,
            [nameof(personalisation)] = personalisation,
            [nameof(clientReference)] = clientReference,
        }, underlyingNotificationClient.SendLetterAsync, templateId, personalisation, clientReference);

        public Task<LetterNotificationResponse> SendPrecompiledLetterAsync(string clientReference, byte[] pdfContents, string postage) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(SendPrecompiledLetterAsync),
            [nameof(clientReference)] = clientReference,
            [nameof(pdfContents)] = pdfContents,
            [nameof(postage)] = postage,
        }, underlyingNotificationClient.SendPrecompiledLetterAsync, clientReference, pdfContents, postage);

        public Task<SmsNotificationResponse> SendSmsAsync(string mobileNumber, string templateId, Dictionary<string, dynamic> personalisation = null, string clientReference = null, string smsSenderId = null) => LogAndHandle(new Dictionary<string, object>
        {
            [MethodNameKey] = nameof(SendSmsAsync),
            [nameof(mobileNumber)] = mobileNumber,
            [nameof(templateId)] = templateId,
            [nameof(personalisation)] = personalisation,
            [nameof(clientReference)] = clientReference,
            [nameof(smsSenderId)] = smsSenderId
        }, underlyingNotificationClient.SendSmsAsync, mobileNumber, templateId, personalisation, clientReference, smsSenderId);

        private const string MethodNameKey = "Method";
        private const string RequestLog = "Sending Request";
        private const string ResponseLog = "Response received in {timeTakenInMs}";

        private async Task<TResult> LogAndHandle<T1, TResult>(Dictionary<string, object> logParameters, Func<T1, Task<TResult>> method, T1 param1)
        {
            using var loggerScope = logger.BeginScope(logParameters);
            logger.LogDebug(RequestLog);
            var sw = Stopwatch.StartNew();
            var result = await method(param1);
            logger.LogDebug(ResponseLog, sw.ElapsedMilliseconds);
            return result;
        }
        private async Task<TResult> LogAndHandle<T1, T2, TResult>(Dictionary<string, object> logParameters, Func<T1, T2, Task<TResult>> method, T1 param1, T2 param2)
        {
            using var loggerScope = logger.BeginScope(logParameters);
            logger.LogDebug(RequestLog);
            var sw = Stopwatch.StartNew();
            var result = await method(param1, param2);
            logger.LogDebug(ResponseLog, sw.ElapsedMilliseconds);
            return result;
        }
        private async Task<TResult> LogAndHandle<T1, T2, T3, TResult>(Dictionary<string, object> logParameters, Func<T1, T2, T3, Task<TResult>> method, T1 param1, T2 param2, T3 param3)
        {
            using var loggerScope = logger.BeginScope(logParameters);
            logger.LogDebug(RequestLog);
            var sw = Stopwatch.StartNew();
            var result = await method(param1, param2, param3);
            logger.LogDebug(ResponseLog, sw.ElapsedMilliseconds);
            return result;
        }
        
        private async Task<TResult> LogAndHandle<T1, T2, T3, T4, T5, TResult>(Dictionary<string, object> logParameters, Func<T1, T2, T3, T4, T5, Task<TResult>> method, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            using var loggerScope = logger.BeginScope(logParameters);
            logger.LogDebug(RequestLog);
            var sw = Stopwatch.StartNew();
            var result = await method(param1, param2, param3, param4, param5);
            logger.LogDebug(ResponseLog, sw.ElapsedMilliseconds);
            return result;
        }
    }
}
