using Microsoft.Extensions.Logging;

namespace NotificationApi.Common.Logging
{
    public static partial class CreateNotificationServiceLogger
    {
        [LoggerMessage(EventId = 4001, Level = LogLevel.Debug, Message = "Attempting notify with template: {Id}")]
        public static partial void LogAttemptingNotify(this ILogger logger, string id);

        [LoggerMessage(EventId = 4002, Level = LogLevel.Debug, Message = "Contact email: {Email}")]
        public static partial void LogContactEmail(this ILogger logger, string email);

        [LoggerMessage(EventId = 4003, Level = LogLevel.Debug, Message = "Parameters {Key}: {Value}")]
        public static partial void LogParameters(this ILogger logger, string key, string value);

        [LoggerMessage(EventId = 4004, Level = LogLevel.Warning,
            Message =
                "Failed to send email to the NotifyAPI for notificationId {NotificationId}. Retrying attempt {RetryAttempt}")]
        public static partial void LogRetryAttempt(this ILogger logger, string notificationId, int retryAttempt);
    }
}
