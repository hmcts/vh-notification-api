using FluentValidation;
using NotificationApi.Contract.Requests;

namespace NotificationApi.Validations;

public class NewUserWelcomeEmailRequestValidation : AbstractValidator<NewUserWelcomeEmailRequest>
{
    public static readonly string UnsupportedRoleMessage =
        "Only participants with the role 'Individual' is currently supported";
    public NewUserWelcomeEmailRequestValidation()
    {
        RuleFor(x => x.HearingId).NotEmpty();
        RuleFor(x => x.ContactEmail).EmailAddress();
        RuleFor(x => x.ParticipantId).NotEmpty();

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.CaseNumber).NotEmpty();
        RuleFor(x => x.CaseName).NotEmpty();
            
        // RoleName must be a RoleNames.Individual or RoleNames.Representative
        RuleFor(x => x.RoleName).Must(x => x == RoleNames.Individual).WithMessage(UnsupportedRoleMessage);
    }
}
