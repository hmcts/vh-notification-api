using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notify.Models.Responses;

namespace NotificationApi.Controllers
{
    [Produces("application/json")]
    [Route("Notification")]
    [ApiController]
    public class CreateNotificationController : ControllerBase
    {
        [HttpPost("template/{notificationType}")]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.OK)]
        public async Task<NotificationResponse> CreateNewNotification(int notificationType)
        {
            throw new NotImplementedException();
        }
    }
}
