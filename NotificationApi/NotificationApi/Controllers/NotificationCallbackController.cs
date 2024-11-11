using Microsoft.AspNetCore.Authorization;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Commands.Core;

namespace NotificationApi.Controllers;

[Produces("application/json")]
[Route("notification")]
[ApiController]
public class NotificationCallbackController(ICommandHandler commandHandler) : ControllerBase
{

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
        
        await commandHandler.Handle(command);
        return Ok();
    }
}
