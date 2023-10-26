using FluentValidation;
using NotificationApi.Contract.Requests;

namespace NotificationApi.Validations;

public class NewUserHearingConfirmationRequestValidation : AbstractValidator<NewUserHearingConfirmationRequest>
{
    public static readonly string UnsupportedRoleMessage =
        "Only participants with the role 'Individual' is currently supported";
    public NewUserHearingConfirmationRequestValidation()
    {
        RuleFor(x => x.HearingId).NotEmpty();
        RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
        RuleFor(x => x.ParticipantId).NotEmpty();
            
        RuleFor(x => x.CaseNumber).NotEmpty();
        RuleFor(x => x.CaseName).NotEmpty();
        RuleFor(x => x.ScheduledDateTime).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.RandomPassword).NotEmpty();
        
        // RoleName must be a RoleNames.Individual or RoleNames.Representative
        RuleFor(x => x.RoleName).Must(x => x == RoleNames.Individual).WithMessage(UnsupportedRoleMessage);
    }
}
