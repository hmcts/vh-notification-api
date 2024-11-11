using System.Threading.Tasks;

namespace NotificationApi.DAL.Queries.Core;

public class QueryHandler(IQueryHandlerFactory queryHandlerFactory) : IQueryHandler
{
    public Task<TResult> Handle<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
    {
        var handler = queryHandlerFactory.Create<TQuery, TResult>(query);
        return handler.Handle(query);
    }
}
