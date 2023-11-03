using NotificationApi.DAL.Commands;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

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
        /// Send an email with information about their new hearings account
        /// </summary>
        [HttpPost("participant-created-account-email")]
        [OpenApiOperation("SendParticipantCreatedAccountEmail")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendParticipantCreatedAccountEmailAsync(SignInDetailsEmailRequest request)
        {
            var notificationType = request.RoleName switch
            {
                RoleNames.Individual => NotificationType.CreateIndividual,
                RoleNames.Representative => NotificationType.CreateRepresentative,
                _ => throw new BadRequestException($"Provided role is not {request.RoleName}")
            };

            var parameters = NotificationParameterMapper.MapToV1AccountCreated(request);

            await ProcessRequest(request.ContactEmail, null, null, notificationType, parameters);
            return Ok();
        }

        /// <summary>
        /// Send an email with a new temporary password
        /// </summary>
        [HttpPost("reset-password-email")]
        [OpenApiOperation("SendResetPasswordEmail")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendResetPasswordEmailAsync(PasswordResetEmailRequest request)
        {
            var notificationType = NotificationType.PasswordReset;
            var parameters = NotificationParameterMapper.MapToPasswordReset(request);

            await ProcessRequest(request.ContactEmail, null, null, notificationType, parameters);
            return Ok();
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
                _ => throw new BadRequestException($"Only LIPs are supported, provided role is {request.RoleName}")
            };

            var parameters = NotificationParameterMapper.MapToWelcomeEmail(request);

            await ProcessRequest(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parameters);

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
                _ => throw new BadRequestException($"Only LIPs are supported, provided role is {request.RoleName}")
            };

            var parameters = NotificationParameterMapper.MapToSingleDayConfirmationNewUser(request);

            await ProcessRequest(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parameters);
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
            var notificationType = request.RoleName switch
            {
                RoleNames.Individual => NotificationType.NewUserLipConfirmationMultiDay,
                _ => throw new BadRequestException($"Only LIPs are supported, provided role is {request.RoleName}")
            };

            var parameters = NotificationParameterMapper.MapToMultiDayConfirmationNewUser(request);

            await ProcessRequest(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parameters);
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
                _ => throw new BadRequestException($"Provided role is not {request.RoleName}")
            };

            var parameters = NotificationParameterMapper.MapToSingleDayConfirmationExistingUser(request);

            if (request.RoleName == RoleNames.Judge)
            {
                parameters.Add(NotifyParams.Judge, request.DisplayName);
                parameters.Add(NotifyParams.CourtroomAccountUserName, request.Username);
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

            await ProcessRequest(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parameters);

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
                _ => throw new BadRequestException($"Provided role is not {request.RoleName}")
            };

            var parameters = NotificationParameterMapper.MapToMultiDayConfirmationForExistingUser(request);

            if (request.RoleName == RoleNames.Judge)
            {
                parameters.Add(NotifyParams.Judge, request.DisplayName);
                parameters.Add(NotifyParams.CourtroomAccountUserName, request.Username.ToLower());
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

            await ProcessRequest(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parameters);

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
                _ => throw new BadRequestException($"Provided role is not {request.RoleName}")
            };

            var parameters = NotificationParameterMapper.MapToSingleDayReminder(request);

            if (request.RoleName == RoleNames.JudicialOfficeHolder)
            {
                parameters.Add(NotifyParams.JudicialOfficeHolder, request.Name);
            }

            if (request.RoleName == RoleNames.Representative)
            {
                parameters.Add(NotifyParams.ClientName, request.Representee);
                parameters.Add(NotifyParams.SolicitorName, request.Name);
            }

            await ProcessRequest(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parameters);
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
            var notificationType = request.RoleName switch
            {
                RoleNames.Individual => NotificationType.NewHearingReminderLipMultiDay,
                _ => throw new BadRequestException($"Provided role is not {request.RoleName}")
            };

            var parameters = NotificationParameterMapper.MapToMultiDayReminder(request);

            await ProcessRequest(request.ContactEmail, request.ParticipantId, request.HearingId,
                notificationType, parameters);

            return Ok();
        }

        private async Task ProcessRequest(string contactEmail, Guid? participantId,
            Guid? hearingId, NotificationType notificationType, Dictionary<string, string> parameters)
        {
            var parametersJson = JsonConvert.SerializeObject(parameters);

            var hasNotificationAlreadyBeenSent = await HasNotificationAlreadyBeenSent(contactEmail,
                participantId, hearingId, notificationType, parametersJson);
            if (hasNotificationAlreadyBeenSent) return;

            await SaveAndSendNotification(contactEmail, participantId, hearingId,
                notificationType, parametersJson, parameters);
        }

        private async Task<bool> HasNotificationAlreadyBeenSent(string contactEmail, Guid? participantId,
            Guid? hearingId, NotificationType notificationType,
            string parametersJson)
        {
            var emailNotifications = await _queryHandler.Handle<GetEmailNotificationQuery, IList<EmailNotification>>(
                new GetEmailNotificationQuery(hearingId, participantId, notificationType, contactEmail));

            // check if notification already exists with the same param values
            return emailNotifications.Any(x => x.Parameters == parametersJson);
        }

        /// <summary>
        /// Add a record to the database to track the notification and send the notification to Notify
        /// </summary>
        private async Task SaveAndSendNotification(string contactEmail, Guid? participantId, Guid? hearingId,
            NotificationType notificationType,
            string parametersJson, Dictionary<string, string> parameters)
        {
            await AreAllParamsGiven(parameters, notificationType);
            var notification = new CreateEmailNotificationCommand(notificationType,
                contactEmail, participantId, hearingId, parametersJson);
            await _createNotificationService.CreateEmailNotificationAsync(notification, parameters);
        }

        private async Task AreAllParamsGiven(Dictionary<string, string> parameters, NotificationType notificationType)
        {
            var template =
                await _queryHandler.Handle<GetTemplateByNotificationTypeQuery, Template>(
                    new GetTemplateByNotificationTypeQuery(notificationType));
            if (template == null)
            {
                throw new BadRequestException($"Invalid {nameof(notificationType)}: {notificationType}");
            }

            var paramNames = template.Parameters.Split(',').Select(x => x.Trim()).ToList();
            var missingParams = paramNames.Where(x => !parameters.ContainsKey(x)).ToList();
            if (missingParams.Any())
            {
                throw new BadRequestException($"Missing parameters: {string.Join(", ", missingParams)}");
            }
        }
    }
}
