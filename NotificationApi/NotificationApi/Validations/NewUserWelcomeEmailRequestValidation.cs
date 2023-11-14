namespace NotificationApi.Validations
{
    public class NewUserWelcomeEmailRequestValidation : AbstractValidator<NewUserWelcomeEmailRequest>
    {
        public NewUserWelcomeEmailRequestValidation()
        {
            RuleFor(x => x.HearingId).NotEmpty();
            RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.ParticipantId).NotEmpty();

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CaseNumber).NotEmpty();
            RuleFor(x => x.CaseName).NotEmpty();
        }
    }
}
