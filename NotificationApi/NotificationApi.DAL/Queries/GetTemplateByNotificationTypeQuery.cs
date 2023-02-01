using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL.Exceptions;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Queries
{
    public class GetTemplateByNotificationTypeQuery : IQuery
    {
        public NotificationType NotificationType { get; }

        public GetTemplateByNotificationTypeQuery(NotificationType notificationType)
        {
            NotificationType = notificationType;
        }
    }

    public class GetTemplateByNotificationTypeQueryHandler : IQueryHandler<GetTemplateByNotificationTypeQuery, Template>
    {
        private readonly NotificationsApiDbContext _notificationsApiDbContext;

        public GetTemplateByNotificationTypeQueryHandler(NotificationsApiDbContext notificationsApiDbContext)
        {
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        public async Task<Template> Handle(GetTemplateByNotificationTypeQuery query)
        {
            var templates = await _notificationsApiDbContext.Templates
                .Where(t => t.NotificationType == query.NotificationType).ToListAsync();

            if (templates.Count > 1)
            {
                throw new DuplicateNotificationTemplateException(query.NotificationType);
            }

            return templates.FirstOrDefault();
        }
    }
}
