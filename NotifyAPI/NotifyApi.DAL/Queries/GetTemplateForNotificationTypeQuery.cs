using System.Threading.Tasks;
using NotifyApi.DAL.Queries.Core;
using NotifyApi.Domain;

namespace NotifyApi.DAL.Queries
{
    public class GetTemplateForNotificationTypeQuery : IQuery
    {

    }

    public class
        GetTemplateForNotificationTypeQueryHandler : IQueryHandler<GetTemplateForNotificationTypeQuery, Template>
    {
        private readonly NotifyApiDbContext _context;

        public GetTemplateForNotificationTypeQueryHandler(NotifyApiDbContext context)
        {
            _context = context;
        }

        public Task<Template> Handle(GetTemplateForNotificationTypeQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
