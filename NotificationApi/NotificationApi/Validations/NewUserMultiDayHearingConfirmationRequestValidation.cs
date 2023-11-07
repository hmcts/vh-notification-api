namespace NotificationApi.Validations
{
    public class
        NewUserMultiDayHearingConfirmationRequestValidation : AbstractValidator<
            NewUserMultiDayHearingConfirmationRequest>
    {
        public static readonly string UnsupportedRoleMessage =
            "Only participants with the role 'Individual' is currently supported for the new user multi-day hearing confirmation";
        public NewUserMultiDayHearingConfirmationRequestValidation()
        {
            RuleFor(x => x.HearingId).NotEmpty();
            RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.ParticipantId).NotEmpty();
            
            RuleFor(x => x.CaseNumber).NotEmpty();
            RuleFor(x => x.CaseName).NotEmpty();
            RuleFor(x => x.ScheduledDateTime).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.RandomPassword).NotEmpty();

            RuleFor(x => x.TotalDays).NotEmpty().GreaterThan(0);
            
            // RoleName must be a RoleNames.Individual
            RuleFor(x => x.RoleName).Must(x => x == RoleNames.Individual).WithMessage(UnsupportedRoleMessage);
        }

        
    }
}
