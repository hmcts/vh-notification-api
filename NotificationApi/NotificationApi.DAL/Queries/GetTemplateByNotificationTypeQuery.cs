using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using System.Threading.Tasks;

namespace NotificationApi.DAL.Queries
{
    public class GetTemplateByNotificationTypeQuery : IQuery
    {
        public NotificationType NotificationType { get; }

        public GetTemplateByNotificationTypeQuery(NotificationType notificationType)
        {
            this.NotificationType = notificationType;
        }
    }

    public class GetTemplateByNotificationTypeQueryHandler : IQueryHandler<GetTemplateByNotificationTypeQuery, Template>
    {
        private readonly NotificationsApiDbContext _notificationsApiDbContext;

        public GetTemplateByNotificationTypeQueryHandler(NotificationsApiDbContext notificationsApiDbContext)
        {
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        public Task<Template> Handle(GetTemplateByNotificationTypeQuery query) => _notificationsApiDbContext.Templates.SingleOrDefaultAsync(t => t.NotificationType == query.NotificationType);
    }
}
