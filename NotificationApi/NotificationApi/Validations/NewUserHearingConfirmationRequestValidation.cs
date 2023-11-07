namespace NotificationApi.Validations;

public class NewUserSingleDayHearingConfirmationRequestValidation : AbstractValidator<NewUserSingleDayHearingConfirmationRequest>
{
    public NewUserSingleDayHearingConfirmationRequestValidation()
    {
        RuleFor(x => x.HearingId).NotEmpty();
        RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
        RuleFor(x => x.ParticipantId).NotEmpty();
            
        RuleFor(x => x.CaseNumber).NotEmpty();
        RuleFor(x => x.CaseName).NotEmpty();
        RuleFor(x => x.ScheduledDateTime).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.RandomPassword).NotEmpty();
    }
}
