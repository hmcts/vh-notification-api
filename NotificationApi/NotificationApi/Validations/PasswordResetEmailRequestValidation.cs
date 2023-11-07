namespace NotificationApi.Validations;

public class PasswordResetEmailRequestValidation : AbstractValidator<PasswordResetEmailRequest>
{
    public PasswordResetEmailRequestValidation()
    {
        RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
