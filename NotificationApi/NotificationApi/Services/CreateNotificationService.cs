using AdminWebsite.Services;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using Notify.Interfaces;
using Notify.Models.Responses;
using Microsoft.Extensions.Logging;
using NotificationApi.Common.Logging;

namespace NotificationApi.Services
{
    public class CreateNotificationService : ICreateNotificationService
    {
        private readonly IAsyncNotificationClient _asyncNotificationClient;
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;
        private readonly IPollyRetryService _pollyRetryService;
        private readonly ILogger<CreateNotificationService> _logger;

        public CreateNotificationService(ICommandHandler commandHandler, IAsyncNotificationClient asyncNotificationClient, 
            IQueryHandler queryHandler, IPollyRetryService pollyRetryService, ILogger<CreateNotificationService> logger)
        {
            _commandHandler = commandHandler;
            _asyncNotificationClient = asyncNotificationClient;
            _queryHandler = queryHandler;
            _pollyRetryService = pollyRetryService;
            _logger = logger;
        }

        public async Task CreateEmailNotificationAsync(CreateEmailNotificationCommand notificationCommand, Dictionary<string, string> parameters)
        {
            var template = await _queryHandler.Handle<GetTemplateByNotificationTypeQuery, Template>(new GetTemplateByNotificationTypeQuery(notificationCommand.NotificationType));

            await _commandHandler.Handle(notificationCommand);
            var requestParameters = parameters.ToDictionary(x => x.Key, x => (dynamic)x.Value);

            var emailNotificationResponse = await SendEmailAsyncRetry(notificationCommand.ContactEmail, template.NotifyTemplateId.ToString(), requestParameters, notificationCommand.NotificationId.ToString());
            await _commandHandler.Handle(new UpdateNotificationSentCommand(notificationCommand.NotificationId, emailNotificationResponse.id, emailNotificationResponse.content.body));
        }

        public async Task<EmailNotificationResponse> SendEmailAsyncRetry(string contactEmail, string notifyTemplateId, Dictionary<string, dynamic> requestParameters, string notificationId)
        {
            var maxRetryAttempts = 2;
            var pauseBetweenFailures = TimeSpan.FromSeconds(5);

            _logger.LogAttemptingNotify(notifyTemplateId);
            _logger.LogContactEmail(contactEmail);
            foreach (var parameter in requestParameters)
                CreateNotificationServiceLogger.LogParameters(_logger, parameter.Key, parameter.Value.ToString());

            var result = await _pollyRetryService.WaitAndRetryAsync<Exception, EmailNotificationResponse>
            (
                maxRetryAttempts,
                _ => pauseBetweenFailures,
                retryAttempt => _logger.LogRetryAttempt(notificationId, retryAttempt),
                callResult => callResult == null,
                () => _asyncNotificationClient.SendEmailAsync(contactEmail, notifyTemplateId, requestParameters, notificationId)
            );
            return result;
        }
    }
}
