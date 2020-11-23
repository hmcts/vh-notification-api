using System.Threading.Tasks;
using NotificationApi.DAL.Models;
using NotificationApi.DAL.Queries.Core;

namespace NotificationApi.DAL.Queries
{
    public class DbHealthCheckQuery : IQuery
    { }

    public class DbHealthCheckQueryHandler: IQueryHandler<DbHealthCheckQuery, DbHealthCheckResult>
    {
        private readonly NotificationsApiDbContext _context;

        public DbHealthCheckQueryHandler(NotificationsApiDbContext context)
        {
            _context = context;
        }
        
        public async Task<DbHealthCheckResult> Handle(DbHealthCheckQuery query)
        {
            return new DbHealthCheckResult
            {
                CanConnect = await _context.Database.CanConnectAsync()
            };
        }
    }
}
