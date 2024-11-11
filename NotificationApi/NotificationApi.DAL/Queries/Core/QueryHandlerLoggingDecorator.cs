using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NotificationApi.Common.Helpers;

namespace NotificationApi.DAL.Queries.Core;

public class QueryHandlerLoggingDecorator<TQuery, TResult>(
    IQueryHandler<TQuery, TResult> underlyingHandler,
    ILogger<TQuery> logger,
    ILoggingDataExtractor loggingDataExtractor)
    : IQueryHandler<TQuery, TResult>
    where TQuery : IQuery
    where TResult : class
{
    public async Task<TResult> Handle(TQuery query)
    {
        var properties = loggingDataExtractor.ConvertToDictionary(query);
        properties.Add(nameof(TQuery), typeof(TQuery).Name);
        properties.Add(nameof(TResult), typeof(TResult).Name);
        using (logger.BeginScope(properties))
        {
            // Unfortunately this scope won't apply to the underlying handler as its already been resolved from the logger factory.
            logger.LogDebug("Handling query");
            var sw = Stopwatch.StartNew();
            var result = await underlyingHandler.Handle(query);
            logger.LogDebug("Handled query in {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
            return result;
        }
    }
}
