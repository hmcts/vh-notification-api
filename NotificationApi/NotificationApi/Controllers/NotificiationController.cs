using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Contract.Responses;
using Notify.Interfaces;

namespace NotificationApi.Controllers
{
    [Produces("application/json")]
    [Route("Notification")]
    [ApiController]
    public class NotificiationController : ControllerBase
    {
        private readonly IAsyncNotificationClient _notificationClient;

        public NotificiationController(IAsyncNotificationClient notificationClient)
        {
            _notificationClient = notificationClient;
        }

        [HttpGet("template/{notificationType}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(NotificationTemplateResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTemplateByNotificationType(int notificationType)
        {
            return Ok(new NotificationTemplateResponse());
        }
    }
}
