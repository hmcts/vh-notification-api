using System.Threading.Tasks;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;

namespace NotificationApi.DAL.Queries
{
    public class GetTemplateForNotificationTypeQuery : IQuery
    {

    }

    public class
        GetTemplateForNotificationTypeQueryHandler : IQueryHandler<GetTemplateForNotificationTypeQuery, Template>
    {
        private readonly NotificationsApiDbContext _context;

        public GetTemplateForNotificationTypeQueryHandler(NotificationsApiDbContext context)
        {
            _context = context;
        }

        public Task<Template> Handle(GetTemplateForNotificationTypeQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
