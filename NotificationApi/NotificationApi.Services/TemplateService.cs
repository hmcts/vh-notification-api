using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using System.Threading.Tasks;

namespace NotificationApi.Services
{
    public interface ITemplateService
    {
        Task<Template> GetTemplateByNotificationType(NotificationType notificationType);
    }

    public class TemplateService : ITemplateService
    {
        private readonly NotificationsApiDbContext _notificationsApiDbContext;
        public TemplateService(NotificationsApiDbContext notificationsApiDbContext)
        {
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        public Task<Template> GetTemplateByNotificationType(NotificationType notificationType)
        {
            return _notificationsApiDbContext.Templates.SingleOrDefaultAsync(t => t.NotificationType == notificationType);
        }
    }
}
