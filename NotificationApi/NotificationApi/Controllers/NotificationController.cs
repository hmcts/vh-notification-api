using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Common;
using NotificationApi.Contract.Responses;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.Controllers
{
    [Produces("application/json")]
    [Route("Notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;

        public NotificationController(IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        [HttpGet("template/{notificationType}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(NotificationTemplateResponse), (int)HttpStatusCode.OK)]
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
    }
}
