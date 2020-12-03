using System.Collections.Generic;
using System.Threading.Tasks;
using NotificationApi.DAL.Commands;

namespace NotificationApi.Services
{
    public interface ICreateNotificationService
    {
        Task CreateEmailNotificationAsync(CreateEmailNotificationCommand notificationCommand, Dictionary<string, string> parameters);
    }
}
