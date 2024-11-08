using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NotificationApi.Common.Helpers;

namespace NotificationApi.DAL.Commands.Core;

public class CommandHandlerLoggingDecorator<TCommand>(
    ICommandHandler<TCommand> underlyingHandler,
    ILogger<TCommand> logger,
    ILoggingDataExtractor loggingDataExtractor)
    : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    public async Task Handle(TCommand command)
    {
        var properties = loggingDataExtractor.ConvertToDictionary(command);
        properties.Add(nameof(TCommand), typeof(TCommand).Name);
        using (logger.BeginScope(properties))
        {
            logger.LogDebug("Handling command");
            var sw = Stopwatch.StartNew();
            await underlyingHandler.Handle(command);
            logger.LogDebug("Handled command in {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
        }
    }
}
