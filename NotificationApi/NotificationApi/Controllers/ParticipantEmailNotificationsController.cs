using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotificationApi.Contract.Requests;
using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using NotificationApi.Services;
using NSwag.Annotations;

namespace NotificationApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class ParticipantEmailNotificationsController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICreateNotificationService _createNotificationService;

        public ParticipantEmailNotificationsController(IQueryHandler queryHandler, ICreateNotificationService createNotificationService)
        {
            _queryHandler = queryHandler;
            _createNotificationService = createNotificationService;
        }

        /// <summary>
        /// Send a welcome to VH email to a participant
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-welcome-email")]
        [OpenApiOperation("SendParticipantWelcomeEmail")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendParticipantWelcomeEmailAsync(NewUserWelcomeEmailRequest request)
        {
            // reject participant who is not a LIP
            var notificationType = request.RoleName == RoleNames.Individual
                ? NotificationType.NewUserLipWelcome
                : throw new NotSupportedException($"Only LIPs are supported, provided role is {request.RoleName}");
            
            var parameters = new Dictionary<string, string>
            {
                {NotifyParams.Name, request.Name},
                {NotifyParams.CaseName, request.CaseName},
                {NotifyParams.CaseNumber, request.CaseNumber}
            };
            var parametersJson = JsonConvert.SerializeObject(parameters);
            
            var hasNotificationAlreadyBeenSent = await HasNotificationAlreadyBeenSent(request, notificationType, parametersJson);
            if (hasNotificationAlreadyBeenSent) return Ok();

            await SaveAndSendNotification(request, notificationType, parametersJson, parameters);

            return Ok();
        }


        /// <summary>
        /// Send a hearing confirmation email for a participant that has a new account
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-single-day-hearing-confirmation-email-new-user")]
        [OpenApiOperation("SendParticipantHearingConfirmationForNewUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SendParticipantHearingConfirmationForNewUserAsync(NewUserHearingConfirmationRequest request)
        {
            // reject participant who is not a LIP
            return Task.FromResult<IActionResult>(Ok());
        }
        
        /// <summary>
        /// Send a hearing confirmation email for a participant that already has a user account
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-single-day-hearing-confirmation-email-existing-user")]
        [OpenApiOperation("SendParticipantHearingConfirmationForExistingUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SendParticipantHearingConfirmationForExistingUserAsync(ExistingUserHearingConfirmationRequest request)
        {
            // reject participant who is not a LIP
            return Task.FromResult<IActionResult>(Ok());
        }
        
        /// <summary>
        /// Send a hearing reminder email for a participant
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-hearing-reminder-email")]
        [OpenApiOperation("SendHearingReminderEmail")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SendHearingReminderEmailAsync(HearingReminderRequest request)
        {
            return Task.FromResult<IActionResult>(Ok());
        }
        
        private async Task<bool> HasNotificationAlreadyBeenSent(NewUserWelcomeEmailRequest request, NotificationType notificationType,
            string parametersJson)
        {
            var emailNotifications = await _queryHandler.Handle<GetEmailNotificationQuery, IList<EmailNotification>>(
                new GetEmailNotificationQuery(request.HearingId, request.ParticipantId,
                    notificationType, request.ContactEmail));

            // check if notification already exists with the same params
            return emailNotifications.Any(x => x.Parameters == parametersJson);
        }
        
        /// <summary>
        /// Add a record to the database to track the notification and send the notification to Notify
        /// </summary>
        /// <param name="request"></param>
        /// <param name="notificationType"></param>
        /// <param name="parametersJson"></param>
        /// <param name="parameters"></param>
        private async Task SaveAndSendNotification(NewUserWelcomeEmailRequest request, NotificationType notificationType,
            string parametersJson, Dictionary<string, string> parameters)
        {
            var notification = new CreateEmailNotificationCommand(notificationType,
                request.ContactEmail, request.ParticipantId, request.HearingId, parametersJson);
            await _createNotificationService.CreateEmailNotificationAsync(notification, parameters);
        }
    }
}
