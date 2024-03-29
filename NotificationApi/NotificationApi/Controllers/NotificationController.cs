using Microsoft.AspNetCore.Authorization;
using NotificationApi.Contract.Responses;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

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
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
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

        [HttpPost]
        [OpenApiOperation("CreateNewNotification")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [Obsolete("Please use the journey specific routes")]
        public async Task<IActionResult> CreateNewNotificationAsync(AddNotificationRequest request)
        {
            var parameters = JsonConvert.SerializeObject(request.Parameters);
            var emailNotifications = await _queryHandler.Handle<GetEmailNotificationQuery, IList<EmailNotification>>(
                new GetEmailNotificationQuery(request.HearingId, request.ParticipantId,
                    (NotificationType)request.NotificationType, request.ContactEmail));
            var emailNotification = emailNotifications.SingleOrDefault(x => x.Parameters == parameters);

            if (emailNotification == null)
            {
                var notification = new CreateEmailNotificationCommand((NotificationType)request.NotificationType,
                    request.ContactEmail, request.ParticipantId, request.HearingId, parameters);
                await _createNotificationService.CreateEmailNotificationAsync(notification, request.Parameters);
            }

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
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
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
