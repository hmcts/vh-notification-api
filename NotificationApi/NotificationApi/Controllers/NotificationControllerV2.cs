using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.DAL.Queries.Core;
using NSwag.Annotations;

namespace NotificationApi.Controllers
{
    [Produces("application/json")]
    [Route("v{version:apiVersion}/notification")]
    [ApiController]
    [ApiVersion("2.0")]
    public class NotificationControllerV2 : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;

        public NotificationControllerV2(IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        [MapToApiVersion("2.0")]
        [HttpGet("template/{notificationType}")]
        [OpenApiOperation("GetTemplateByNotificationType")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public IActionResult GetTemplateByNotificationTypeAsync(Contract.NotificationType notificationType)
        {
            return Ok("V2");
        }
    }
}
