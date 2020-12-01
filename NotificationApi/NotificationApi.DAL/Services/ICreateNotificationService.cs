using NotificationApi.DAL.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationApi.DAL.Services
{
    public interface ICreateNotificationService
    {
        Task CreateEmailNotificationAsync(CreateEmailNotificationCommand notificationCommand, Dictionary<string, string> parameters);
    }
}
