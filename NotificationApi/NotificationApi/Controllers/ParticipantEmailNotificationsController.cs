using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotificationApi.Common.Util;
using NotificationApi.DAL.Commands;
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
    [ApiController]
    public class ParticipantEmailNotificationsController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICreateNotificationService _createNotificationService;
        private readonly IFeatureToggles _featureToggles;

        public ParticipantEmailNotificationsController(IQueryHandler queryHandler,
            ICreateNotificationService createNotificationService, IFeatureToggles featureToggles)
        {
            _queryHandler = queryHandler;
            _createNotificationService = createNotificationService;
            _featureToggles = featureToggles;
        }

        /// <summary>
        /// Send a welcome to VH email to a participant
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-welcome-email")]
        [OpenApiOperation("SendParticipantWelcomeEmail")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendParticipantWelcomeEmailAsync(NewUserWelcomeEmailRequest request)
        {
            var notificationType = request.RoleName switch
            {
                RoleNames.Individual => NotificationType.NewUserLipWelcome,
                _ => throw new NotSupportedException($"Only LIPs are supported, provided role is {request.RoleName}")
            };

            var parameters = new Dictionary<string, string>
            {
                {NotifyParams.Name, request.Name},
                {NotifyParams.CaseName, request.CaseName},
                {NotifyParams.CaseNumber, request.CaseNumber}
            };
            var parametersJson = JsonConvert.SerializeObject(parameters);

            var hasNotificationAlreadyBeenSent = await HasNotificationAlreadyBeenSent(request.ContactEmail,
                request.ParticipantId, request.HearingId, notificationType, parametersJson);
            if (hasNotificationAlreadyBeenSent) return Ok();

            await SaveAndSendNotification(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parametersJson, parameters);

            return Ok();
        }

        /// <summary>
        /// Send a single day hearing confirmation email for a participant that has a new account
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-single-day-hearing-confirmation-email-new-user")]
        [OpenApiOperation("SendParticipantSingleDayHearingConfirmationForNewUserEmail")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendParticipantSingleDayHearingConfirmationForNewUserEmailAsync(
            NewUserSingleDayHearingConfirmationRequest request)
        {
            var notificationType = request.RoleName switch
            {
                RoleNames.Individual => NotificationType.NewUserLipConfirmation,
                _ => throw new NotSupportedException($"Only LIPs are supported, provided role is {request.RoleName}")
            };

            var parameters = new Dictionary<string, string>
            {
                {NotifyParams.CaseName, request.CaseName},
                {NotifyParams.CaseNumber, request.CaseNumber},
                {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
                {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()},
                {NotifyParams.Name, request.Name},
                {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.UserName, request.Username.ToLower()},
                {NotifyParams.RandomPassword, request.RandomPassword}
            };

            var parametersJson = JsonConvert.SerializeObject(parameters);

            var hasNotificationAlreadyBeenSent = await HasNotificationAlreadyBeenSent(request.ContactEmail,
                request.ParticipantId, request.HearingId, notificationType, parametersJson);
            if (hasNotificationAlreadyBeenSent) return Ok();

            await SaveAndSendNotification(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parametersJson, parameters);

            return Ok();
        }

        /// <summary>
        /// Send a multi-day hearing confirmation email for a participant that has a new account
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-multi-day-hearing-confirmation-email-new-user")]
        [OpenApiOperation("SendParticipantMultiDayHearingConfirmationForNewUserEmail")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendParticipantMultiDayHearingConfirmationForNewUserEmailAsync(
            NewUserMultiDayHearingConfirmationRequest request)
        {
            // should I return bad request when _featureToggles.UsePostMay2023Template() is false?   
            var notificationType = request.RoleName switch
            {
                RoleNames.Individual => NotificationType.NewUserLipConfirmationMultiDay,
                _ => throw new NotSupportedException($"Only LIPs are supported, provided role is {request.RoleName}")
            };

            var parameters = new Dictionary<string, string>
            {
                {NotifyParams.CaseName, request.CaseName},
                {NotifyParams.CaseNumber, request.CaseNumber},
                {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
                {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()},
                {NotifyParams.Name, request.Name},
                {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.UserName, request.Username.ToLower()},
                {NotifyParams.RandomPassword, request.RandomPassword},
                {NotifyParams.TotalDays, request.TotalDays.ToString()}
            };

            var parametersJson = JsonConvert.SerializeObject(parameters);

            var hasNotificationAlreadyBeenSent = await HasNotificationAlreadyBeenSent(request.ContactEmail,
                request.ParticipantId, request.HearingId, notificationType, parametersJson);
            if (hasNotificationAlreadyBeenSent) return Ok();

            await SaveAndSendNotification(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parametersJson, parameters);

            return Ok();
        }

        /// <summary>
        /// Send a single day hearing confirmation email for a participant that already has a user account
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-single-day-hearing-confirmation-email-existing-user")]
        [OpenApiOperation("SendParticipantSingleDayHearingConfirmationForExistingUserEmail")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendParticipantSingleDayHearingConfirmationForExistingUserEmailAsync(
            ExistingUserSingleDayHearingConfirmationRequest request)
        {
            var useNewTemplates = _featureToggles.UsePostMay2023Template();
            NotificationType notificationType = request.RoleName switch
            {
                RoleNames.Individual when !useNewTemplates => NotificationType.HearingConfirmationLip,
                RoleNames.Individual when useNewTemplates => NotificationType.ExistingUserLipConfirmation,
                RoleNames.Representative => NotificationType.HearingConfirmationRepresentative,
                RoleNames.JudicialOfficeHolder when !request.HasAJudiciaryUsername() => NotificationType
                    .HearingConfirmationJoh,
                RoleNames.JudicialOfficeHolder when request.HasAJudiciaryUsername() => NotificationType
                    .HearingConfirmationEJudJoh,
                RoleNames.Judge when !request.HasAJudiciaryUsername() => NotificationType.HearingConfirmationJudge,
                RoleNames.Judge when request.HasAJudiciaryUsername() => NotificationType.HearingConfirmationEJudJudge,
                _ => throw new NotSupportedException($"Provided role is not {request.RoleName}")
            };

            var parameters = new Dictionary<string, string>
            {
                {NotifyParams.Name, request.Name},
                {NotifyParams.CaseName, request.CaseName},
                {NotifyParams.CaseNumber, request.CaseNumber},
                {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
                {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.UserName, request.Username.ToLower()},
                {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()}
            };


            if (request.RoleName == RoleNames.Judge)
            {
                parameters.Add(NotifyParams.Judge, request.DisplayName);
            }

            if (request.RoleName == RoleNames.JudicialOfficeHolder)
            {
                parameters.Add(NotifyParams.JudicialOfficeHolder, request.Name);
            }

            if (request.RoleName == RoleNames.Representative)
            {
                parameters.Add(NotifyParams.ClientName, request.Representee);
                parameters.Add(NotifyParams.SolicitorName, request.Name);
            }

            var parametersJson = JsonConvert.SerializeObject(parameters);

            var hasNotificationAlreadyBeenSent = await HasNotificationAlreadyBeenSent(request.ContactEmail,
                request.ParticipantId, request.HearingId, notificationType, parametersJson);
            if (hasNotificationAlreadyBeenSent) return Ok();

            await SaveAndSendNotification(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parametersJson, parameters);

            return Ok();
        }

        /// <summary>
        /// Send a multi-day hearing confirmation email for a participant that already has a user account
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-multi-day-hearing-confirmation-email-existing-user")]
        [OpenApiOperation("SendParticipantMultiDayHearingConfirmationForExistingUserEmail")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendParticipantMultiDayHearingConfirmationForExistingUserEmailAsync(
            ExistingUserMultiDayHearingConfirmationRequest request)
        {
            var useNewTemplates = _featureToggles.UsePostMay2023Template();
            var notificationType = request.RoleName switch
            {
                RoleNames.Individual when useNewTemplates => NotificationType.ExistingUserLipConfirmationMultiDay,
                RoleNames.Individual when !useNewTemplates => NotificationType.HearingConfirmationLipMultiDay,
                RoleNames.Representative => NotificationType.HearingConfirmationRepresentativeMultiDay,
                RoleNames.JudicialOfficeHolder when !request.HasAJudiciaryUsername() => NotificationType
                    .HearingConfirmationJohMultiDay,
                RoleNames.JudicialOfficeHolder when request.HasAJudiciaryUsername() => NotificationType
                    .HearingConfirmationEJudJohMultiDay,
                RoleNames.Judge when !request.HasAJudiciaryUsername() => NotificationType
                    .HearingConfirmationJudgeMultiDay,
                RoleNames.Judge when request.HasAJudiciaryUsername() => NotificationType
                    .HearingConfirmationEJudJudgeMultiDay,
                _ => throw new NotSupportedException($"Provided role is not {request.RoleName}")
            };

            var parameters = new Dictionary<string, string>
            {
                {NotifyParams.CaseName, request.CaseName},
                {NotifyParams.CaseNumber, request.CaseNumber},
                {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
                {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()},
                {NotifyParams.Name, request.Name},
                {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.UserName, request.Username.ToLower()},
                {NotifyParams.TotalDays, request.TotalDays.ToString()},
            };

            if (request.RoleName == RoleNames.Judge)
            {
                parameters.Add(NotifyParams.Judge, request.DisplayName);
            }

            if (request.RoleName == RoleNames.JudicialOfficeHolder)
            {
                parameters.Add(NotifyParams.JudicialOfficeHolder, request.Name);
            }

            if (request.RoleName == RoleNames.Representative)
            {
                parameters.Add(NotifyParams.ClientName, request.Representee);
                parameters.Add(NotifyParams.SolicitorName, request.Name);
            }



            var parametersJson = JsonConvert.SerializeObject(parameters);

            var hasNotificationAlreadyBeenSent = await HasNotificationAlreadyBeenSent(request.ContactEmail,
                request.ParticipantId, request.HearingId, notificationType, parametersJson);
            if (hasNotificationAlreadyBeenSent) return Ok();

            await SaveAndSendNotification(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parametersJson, parameters);

            return Ok();
        }

        /// <summary>
        /// Send a single day hearing reminder email for a participant
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-single-day-hearing-reminder-email")]
        [OpenApiOperation("SendSingleDayHearingReminderEmail")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendSingleDayHearingReminderEmailAsync(SingleDayHearingReminderRequest request)
        {
            var useNewTemplates = _featureToggles.UsePostMay2023Template();
            var notificationType = request.RoleName switch
            {
                RoleNames.Individual when useNewTemplates => NotificationType.NewHearingReminderLIP,
                RoleNames.Individual when !useNewTemplates => NotificationType.NewHearingReminderLipSingleDay,
                RoleNames.Representative => NotificationType.NewHearingReminderRepresentative,
                RoleNames.JudicialOfficeHolder when !request.HasAJudiciaryUsername() => NotificationType
                    .NewHearingReminderJOH,
                RoleNames.JudicialOfficeHolder when request.HasAJudiciaryUsername() => NotificationType
                    .NewHearingReminderEJUD,
                _ => throw new NotSupportedException($"Provided role is not {request.RoleName}")
            };

            var parameters = new Dictionary<string, string>
            {
                {NotifyParams.CaseName, request.CaseName},
                {NotifyParams.CaseNumber, request.CaseNumber},
                {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
                {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()},
                {NotifyParams.Name, request.Name},
                {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.UserName, request.Username.ToLower()},
            };

            if (request.RoleName == RoleNames.JudicialOfficeHolder)
            {
                parameters.Add(NotifyParams.JudicialOfficeHolder, request.Name);
            }

            if (request.RoleName == RoleNames.Representative)
            {
                parameters.Add(NotifyParams.ClientName, request.Representee);
                parameters.Add(NotifyParams.SolicitorName, request.Name);
            }


            var parametersJson = JsonConvert.SerializeObject(parameters);

            var hasNotificationAlreadyBeenSent = await HasNotificationAlreadyBeenSent(request.ContactEmail,
                request.ParticipantId, request.HearingId, notificationType, parametersJson);
            if (hasNotificationAlreadyBeenSent) return Ok();

            await SaveAndSendNotification(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parametersJson, parameters);

            return Ok();
        }

        /// <summary>
        /// Send a multi day hearing reminder email for a participant
        /// </summary>
        /// <returns></returns>
        [HttpPost("participant-multi-day-hearing-reminder-email")]
        [OpenApiOperation("SendMultiDayHearingReminderEmail")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendMultiDayHearingReminderEmailAsync(MultiDayHearingReminderRequest request)
        {
            // should I return bad request when _featureToggles.UsePostMay2023Template() is false?   
            var notificationType = request.RoleName switch
            {
                RoleNames.Individual => NotificationType.NewHearingReminderLipMultiDay,
                _ => throw new NotSupportedException($"Provided role is not {request.RoleName}")
            };

            var parameters = new Dictionary<string, string>
            {
                {NotifyParams.CaseName, request.CaseName},
                {NotifyParams.CaseNumber, request.CaseNumber},
                {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
                {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()},
                {NotifyParams.Name, request.Name},
                {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
                {NotifyParams.UserName, request.Username.ToLower()},
                {NotifyParams.TotalDays, request.TotalDays.ToString()},
            };

            if (request.RoleName == RoleNames.JudicialOfficeHolder)
            {
                parameters.Add(NotifyParams.JudicialOfficeHolder, request.Name);
            }

            if (request.RoleName == RoleNames.Representative)
            {
                parameters.Add(NotifyParams.ClientName, request.Representee);
                parameters.Add(NotifyParams.SolicitorName, request.Name);
            }

            var parametersJson = JsonConvert.SerializeObject(parameters);

            var hasNotificationAlreadyBeenSent = await HasNotificationAlreadyBeenSent(request.ContactEmail,
                request.ParticipantId, request.HearingId, notificationType, parametersJson);
            if (hasNotificationAlreadyBeenSent) return Ok();

            await SaveAndSendNotification(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parametersJson, parameters);

            return Ok();
        }

        private async Task<bool> HasNotificationAlreadyBeenSent(string contactEmail, Guid? participantId,
            Guid? hearingId, NotificationType notificationType,
            string parametersJson)
        {
            var emailNotifications = await _queryHandler.Handle<GetEmailNotificationQuery, IList<EmailNotification>>(
                new GetEmailNotificationQuery(hearingId, participantId, notificationType, contactEmail));

            // check if notification already exists with the same params
            return emailNotifications.Any(x => x.Parameters == parametersJson);
        }

        /// <summary>
        /// Add a record to the database to track the notification and send the notification to Notify
        /// </summary>
        private async Task SaveAndSendNotification(string contactEmail, Guid? participantId, Guid? hearingId,
            NotificationType notificationType,
            string parametersJson, Dictionary<string, string> parameters)
        {
            var notification = new CreateEmailNotificationCommand(notificationType,
                contactEmail, participantId, hearingId, parametersJson);
            await _createNotificationService.CreateEmailNotificationAsync(notification, parameters);
        }
    }
}
