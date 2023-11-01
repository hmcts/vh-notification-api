namespace NotificationApi.Validations
{
    public class MultiDayHearingReminderRequestValidation : AbstractValidator<MultiDayHearingReminderRequest>
    {
        public MultiDayHearingReminderRequestValidation()
        {
            RuleFor(x => x.HearingId).NotEmpty();
            RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.ParticipantId).NotEmpty();

            RuleFor(x => x.RoleName).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CaseNumber).NotEmpty();
            RuleFor(x => x.CaseName).NotEmpty();
            RuleFor(x => x.ScheduledDateTime).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.TotalDays).NotEmpty().GreaterThan(0);
        
            RuleFor(x=> x.Representee).NotEmpty().When(x => x.RoleName == RoleNames.Representative);
        }
    }
}
