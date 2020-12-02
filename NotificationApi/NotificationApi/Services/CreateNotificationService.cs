using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationApi.Common;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using Notify.Interfaces;

namespace NotificationApi.Services
{
    public class CreateNotificationService : ICreateNotificationService
    {
        private readonly IAsyncNotificationClient _asyncNotificationClient;
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public CreateNotificationService(ICommandHandler commandHandler, IAsyncNotificationClient asyncNotificationClient, IQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _asyncNotificationClient = asyncNotificationClient;
            _queryHandler = queryHandler;
        }

        public async Task CreateEmailNotificationAsync(CreateEmailNotificationCommand notificationCommand, Dictionary<string, string> parameters)
        {
            var template = await _queryHandler.Handle<GetTemplateByNotificationTypeQuery, Template>(new GetTemplateByNotificationTypeQuery(notificationCommand.NotificationType));

            await _commandHandler.Handle(notificationCommand);
            var requestParameters = parameters.ToDictionary(x => x.Key, x => (dynamic)x.Value);
            var emailNotificationResponse = await _asyncNotificationClient.SendEmailAsync(notificationCommand.ContactEmail, template.NotifyTemplateId.ToString(), requestParameters, notificationCommand.NotificationId.ToString());
            await _commandHandler.Handle(new UpdateNotificationSentCommand(notificationCommand.NotificationId, emailNotificationResponse.id, emailNotificationResponse.content.body));
        }
    }
}
