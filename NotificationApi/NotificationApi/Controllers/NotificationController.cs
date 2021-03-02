using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
using NotificationApi.Services;
using NSwag.Annotations;

namespace NotificationApi.Controllers
{
    [Produces("application/json")]
    [Route("notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICommandHandler _commandHandler;
        private readonly ICreateNotificationService _createNotificationService;

        public NotificationController(IQueryHandler queryHandler,
            ICommandHandler commandHandler, ICreateNotificationService createNotificationService)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _createNotificationService = createNotificationService;
        }

        [HttpGet("template/{notificationType}")]
        [OpenApiOperation("GetTemplateByNotificationType")]
        [ProducesResponseType(typeof(NotificationTemplateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTemplateByNotificationTypeAsync(Contract.NotificationType notificationType)
        {
            var template = await _queryHandler.Handle<GetTemplateByNotificationTypeQuery, Template>(new GetTemplateByNotificationTypeQuery((NotificationType)notificationType));
            if (template == null)
            {
                throw new BadRequestException($"Invalid {nameof(notificationType)}: {notificationType}");
            }

            return Ok(new NotificationTemplateResponse
            {
                Id = template.Id,
                NotificationType = (Contract.NotificationType)template.NotificationType,
                NotifyTemplateId = template.NotifyTemplateId,
                Parameters = template.Parameters
            });
        }

        [HttpGet("{email}")]
        [OpenApiOperation("GetPasswordNotificationByEmail")]
        [ProducesResponseType(typeof(List<NotificationResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPasswordNotificationByEmailAsync(string email)
        {
            var notification = await _queryHandler.Handle<GetNotificationByEmailQuery, List<EmailNotification>>(new GetNotificationByEmailQuery(email));
            if (notification == null)
            {
                throw new BadRequestException($"Notification does not exists for {nameof(email)}: {email}");
            }

            var notificationResponses = notification.Select(n => new NotificationResponse { Id = n.Id }).ToList();

            return Ok(notificationResponses);
        }

        [HttpGet("{notificationType}/{hearingId}/{participantId}")]
        [OpenApiOperation("GetNotificationByHearingAndParticipant")]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetNotificationByHearingAndParticipantAsync(Contract.NotificationType notificationType, string hearingId, string participantId)
        {
            var notification = await _queryHandler.Handle<GetNotificationByParticipantAndHearingQuery, EmailNotification>(new GetNotificationByParticipantAndHearingQuery((NotificationType)notificationType, hearingId, participantId));
            if (notification == null)
            {
                throw new BadRequestException($"{nameof(notificationType)}: {notificationType} does not exists for {nameof(hearingId)}: {hearingId}  and {nameof(participantId)}: {participantId}");
            }

            return Ok(new NotificationResponse
            {
                Id = notification.Id
            });
        }


        [HttpPost]
        [OpenApiOperation("CreateNewNotification")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNewNotificationAsync(AddNotificationRequest request)
        {
            var notification = new CreateEmailNotificationCommand((NotificationType)request.NotificationType, request.ContactEmail, request.ParticipantId, request.HearingId);
            await _createNotificationService.CreateEmailNotificationAsync(notification, request.Parameters);            
            return Ok();
        }

        /// <summary>
        /// Process callbacks from Gov Notify API
        /// </summary>
        /// <returns></returns>
        [HttpPost("callback")]
        [OpenApiOperation("HandleCallback")]
        [Authorize(AuthenticationSchemes = "Callback")]
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
