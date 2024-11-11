using Microsoft.AspNetCore.Authorization;
using NotificationApi.Contract.Responses;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.Controllers;

[Produces("application/json")]
[Route("notification")]
[ApiController]
public class NotificationController(IQueryHandler queryHandler, ICreateNotificationService createNotificationService) : ControllerBase
{
    [HttpGet("template/{notificationType}")]
    [OpenApiOperation("GetTemplateByNotificationType")]
    [ProducesResponseType(typeof(NotificationTemplateResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetTemplateByNotificationTypeAsync(Contract.NotificationType notificationType)
    {
        var template = await queryHandler.Handle<GetTemplateByNotificationTypeQuery, Template>(new GetTemplateByNotificationTypeQuery((NotificationType)notificationType));
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
    [Obsolete("Please use the journey specific routes, used to seed tests")]
    public async Task<IActionResult> CreateNewNotificationAsync(AddNotificationRequest request)
    {
        var parameters = JsonConvert.SerializeObject(request.Parameters);
        var emailNotifications = await queryHandler.Handle<GetEmailNotificationQuery, IList<EmailNotification>>(
            new GetEmailNotificationQuery(request.HearingId, request.ParticipantId,
                (NotificationType)request.NotificationType, request.ContactEmail));
        var emailNotification = emailNotifications.SingleOrDefault(x => x.Parameters == parameters);
        
        if (emailNotification == null)
        {
            var notification = new CreateEmailNotificationCommand((NotificationType)request.NotificationType,
                request.ContactEmail, request.ParticipantId, request.HearingId, parameters);
            await createNotificationService.CreateEmailNotificationAsync(notification, request.Parameters);
        }
        
        return Ok();
    }
}
