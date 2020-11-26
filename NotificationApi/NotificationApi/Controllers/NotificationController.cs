using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Common;
using NotificationApi.Contract.Requests;
using NotificationApi.Contract.Responses;
using NotificationApi.DAL;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using NotificationApi.Services;
using Notify.Interfaces;

namespace NotificationApi.Controllers
{
    [Produces("application/json")]
    [Route("Notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        private readonly IAsyncNotificationClient _asyncNotificationClient;
        private readonly NotificationsApiDbContext _notificationsApiDbContext;

        public NotificationController(ITemplateService templateService, IAsyncNotificationClient asyncNotificationClient, NotificationsApiDbContext notificationsApiDbContext)
        {
            _templateService = templateService;
            _asyncNotificationClient = asyncNotificationClient;
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        [HttpGet("template/{notificationType}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(NotificationTemplateResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTemplateByNotificationType(NotificationType notificationType)
        {
            var template = await _templateService.GetTemplateByNotificationType(notificationType);

            return Ok(new NotificationTemplateResponse
            {
                Id = template.Id,
                NotificationType = (int)template.NotificationType,
                NotifyemplateId = template.NotifyTemplateId,
                Parameters = template.Parameters
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(NotificationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateNewNotificationResponse(AddNotificationRequest request)
        {
            var template = await _templateService.GetTemplateByNotificationType((NotificationType)request.NotificationType);
           
            var notification = new EmailNotification((NotificationType)request.NotificationType, request.ContactEmail, request.ParticipantId, request.HearingId);
            _notificationsApiDbContext.Notifications.Add(notification); 
            await _notificationsApiDbContext.SaveChangesAsync();

            var requestParameters = request.Parameters.ToDictionary(x => x.Key, x => (dynamic)x.Value);
            var emailNotificationResponse = await _asyncNotificationClient.SendEmailAsync(request.ContactEmail, template.NotifyTemplateId.ToString(), requestParameters);
            notification.AssignPayload(emailNotificationResponse.content.body);
            notification.AssignExternalId(emailNotificationResponse.id);

            notification.UpdateDeliveryStatus(DeliveryStatus.Created);

            await _notificationsApiDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
