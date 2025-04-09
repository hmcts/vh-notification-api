using Microsoft.Extensions.Logging;

namespace NotificationApi.Common.Logging;

public static partial class CommandHandlerLoggingDecoratorLogger
{
    [LoggerMessage(EventId = 4005, Level = LogLevel.Debug, Message = "Handling command")]
    public static partial void LogHandlingCommand(this ILogger logger);

    [LoggerMessage(EventId = 4006, Level = LogLevel.Debug, Message = "Handled command in {ElapsedMilliseconds}ms")]
    public static partial void LogHandledCommand(this ILogger logger, long elapsedMilliseconds);
}
