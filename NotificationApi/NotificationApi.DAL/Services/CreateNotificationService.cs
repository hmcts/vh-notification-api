using System.Linq;
using System.Threading.Tasks;
using NotificationApi.Contract.Requests;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using Notify.Interfaces;

namespace NotificationApi.DAL.Services
{
    public class CreateNotificationService
    {
        private readonly IAsyncNotificationClient _asyncNotificationClient;
        private readonly ICommandHandler _commandHandler;
        
        public CreateNotificationService(ICommandHandler commandHandler, IAsyncNotificationClient asyncNotificationClient)
        {
            _commandHandler = commandHandler;
            _asyncNotificationClient = asyncNotificationClient;
        }

        public async Task CreateNotificationAsync(AddNotificationRequest request, Template template)
        {
            var notification = new CreateEmailNotificationCommand((NotificationType)request.NotificationType, request.ContactEmail, request.ParticipantId, request.HearingId);
            await _commandHandler.Handle(notification);

            var requestParameters = request.Parameters.ToDictionary(x => x.Key, x => (dynamic)x.Value);
            var emailNotificationResponse = await _asyncNotificationClient.SendEmailAsync(request.ContactEmail, template.NotifyTemplateId.ToString(), requestParameters, notification.NotificationId.ToString());

            await _commandHandler.Handle(new UpdateNotificationSentCommand(notification.NotificationId, emailNotificationResponse.id, emailNotificationResponse.content.body));
        }
    }
}
