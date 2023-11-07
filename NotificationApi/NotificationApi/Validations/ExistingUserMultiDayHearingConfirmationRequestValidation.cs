namespace NotificationApi.Validations
{
    public class
        ExistingUserMultiDayHearingConfirmationRequestValidation : AbstractValidator<
            ExistingUserMultiDayHearingConfirmationRequest>
    {
        public ExistingUserMultiDayHearingConfirmationRequestValidation()
        {
            RuleFor(x => x.HearingId).NotEmpty();
            RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.ParticipantId).NotEmpty();
            
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CaseNumber).NotEmpty();
            RuleFor(x => x.CaseName).NotEmpty();
            RuleFor(x => x.ScheduledDateTime).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.TotalDays).NotEmpty();
            
            RuleFor(x => x.RoleName).NotEmpty();
        
            RuleFor(x=> x.Representee).NotEmpty().When(x => x.RoleName == RoleNames.Representative);
            RuleFor(x=> x.DisplayName).NotEmpty().When(x => x.RoleName == RoleNames.Judge);
        }
    }
}
