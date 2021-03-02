using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Queries
{
    public class GetNotificationByEmailQuery: IQuery
    {
        public NotificationType NotificationType { get; }
        public string Email { get; }
        public GetNotificationByEmailQuery(string email)
        {
            NotificationType = NotificationType.PasswordReset;
            Email = email;
        }
    }

    public class GetNotificationByEmailQueryHandler : IQueryHandler<GetNotificationByEmailQuery, List<EmailNotification>>        
    {
        private readonly NotificationsApiDbContext _notificationsApiDbContext;

        public GetNotificationByEmailQueryHandler(NotificationsApiDbContext notificationsApiDbContext)
        {
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        public Task<List<EmailNotification>> Handle(GetNotificationByEmailQuery query)
        {
            return _notificationsApiDbContext.EmailNotifications.Where(t => t.NotificationType == query.NotificationType
                                                                                && t.ToEmail == query.Email).ToListAsync(); 
        }
    }
}
