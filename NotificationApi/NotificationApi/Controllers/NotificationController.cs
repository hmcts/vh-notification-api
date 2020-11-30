using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Common;
using NotificationApi.Contract.Requests;
using NotificationApi.Contract.Responses;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using NotificationApi.Extensions;
using Notify.Interfaces;

namespace NotificationApi.Controllers
{
    [Produces("application/json")]
    [Route("Notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;
        private readonly IAsyncNotificationClient _asyncNotificationClient;
        private readonly ICommandHandler _commandHandler;

        public NotificationController(IQueryHandler queryHandler, IAsyncNotificationClient asyncNotificationClient,
            ICommandHandler commandHandler)
        {
            _queryHandler = queryHandler;
            _asyncNotificationClient = asyncNotificationClient;
            _commandHandler = commandHandler;
        }

        [HttpGet("template/{notificationType}")]
        [ProducesResponseType(typeof(NotificationTemplateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTemplateByNotificationType(int notificationType)
        {
            var template = await _queryHandler.Handle<GetTemplateByNotificationTypeQuery, Template>(new GetTemplateByNotificationTypeQuery((NotificationType)notificationType));
            if (template == null)
            {
                throw new BadRequestException($"Invalid {nameof(notificationType)}: {notificationType}");
            }

            return Ok(new NotificationTemplateResponse
            {
                Id = template.Id,
                NotificationType = (int)template.NotificationType,
                NotifyemplateId = template.NotifyTemplateId,
                Parameters = template.Parameters
            });
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNewNotificationResponse(AddNotificationRequest request)
        {
            var template = await _queryHandler.Handle<GetTemplateByNotificationTypeQuery, Template>(new GetTemplateByNotificationTypeQuery((NotificationType)request.NotificationType));
            if (template == null)
            {
                throw new BadRequestException($"Invalid {nameof(request.NotificationType)}: {request.NotificationType}");
            }

            var notification = new CreateEmailNotificationCommand(request.NotificationType, request.ContactEmail, request.ParticipantId, request.HearingId);
            await _commandHandler.Handle(notification);

            var requestParameters = request.Parameters.ToDictionary(x => x.Key, x => (dynamic)x.Value);
            var emailNotificationResponse = await _asyncNotificationClient.SendEmailAsync(request.ContactEmail, template.NotifyTemplateId.ToString(), requestParameters, notification.NotificationId.ToString());

            await _commandHandler.Handle(new UpdateNotificationSentCommand(notification.NotificationId, emailNotificationResponse.id, emailNotificationResponse.content.body));

            return Ok();
        }

        /// <summary>
        /// Process callbacks from Gov Notify API
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> HandleCallbackAsync(NotificationCallbackRequest notificationCallbackRequest)
        {
            var notificationId = notificationCallbackRequest.ReferenceAsGuid();
            var deliveryStatus = notificationCallbackRequest.DeliveryStatusAsEnum();
            var externalId = notificationCallbackRequest.Id;
            var command = new UpdateNotificationDeliveryStatusCommand(notificationId, externalId, deliveryStatus);

            await _commandHandler.Handle(command);
            return Ok();
        }
    }
}
