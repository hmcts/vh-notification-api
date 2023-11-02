namespace NotificationApi.Validations;

public class SignInDetailsEmailRequestValidation : AbstractValidator<SignInDetailsEmailRequest>
{
    public SignInDetailsEmailRequestValidation()
    {
        RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.RoleName).NotEmpty();
    }
}
