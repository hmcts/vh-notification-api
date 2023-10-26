using FluentValidation;
using NotificationApi.Contract.Requests;

namespace NotificationApi.Validations;

public class ExistingUserHearingConfirmationRequestValidation : AbstractValidator<ExistingUserHearingConfirmationRequest>
{
    public static readonly string UnsupportedRoleMessage =
        "Only participants with the role 'Individual' is currently supported";
    public ExistingUserHearingConfirmationRequestValidation()
    {
        RuleFor(x => x.HearingId).NotEmpty();
        RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
        RuleFor(x => x.ParticipantId).NotEmpty();
            
        RuleFor(x => x.CaseNumber).NotEmpty();
        RuleFor(x => x.CaseName).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.ScheduledDateTime).NotEmpty();
            
        // RoleName must be a RoleNames.Individual or RoleNames.Representative
        RuleFor(x => x.RoleName).Must(x => x == RoleNames.Individual).WithMessage(UnsupportedRoleMessage);
    }
}
