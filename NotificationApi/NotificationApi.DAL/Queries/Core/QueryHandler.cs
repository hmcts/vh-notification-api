using System.Threading.Tasks;

namespace NotificationApi.DAL.Queries.Core
{
    public class QueryHandler : IQueryHandler
    {
        private readonly IQueryHandlerFactory _queryHandlerFactory;

        public QueryHandler(IQueryHandlerFactory queryHandlerFactory)
        {
            _queryHandlerFactory = queryHandlerFactory;
        }

        public Task<TResult> Handle<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            var handler = _queryHandlerFactory.Create<TQuery, TResult>(query);
            return handler.Handle(query);
        }
    }
}
