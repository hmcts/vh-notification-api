using NotificationApi.Contract;

namespace NotificationApi.Validations
{
    public class AddNotificationRequestValidation : AbstractValidator<AddNotificationRequest>
    {
        public const string MissingParametersMessage = "Parameters are required";
        public const string MissingEmailMessage = "Email is required";
        public const string MissingHearingIdMessage = "HearingId is required";
        public const string InvalidMessageTypeMessage = "Message type is invalid";
        public const string InvalidNotificationTypeMessage = "Notification type is invalid";
        public const string MissingParticipantIdMessage = "Participant is required";
        public const string MissingPhoneNumberMessage = "Phone number is required";
        
        public AddNotificationRequestValidation()
        {
            RuleFor(x => x.Parameters).NotEmpty().WithMessage(MissingParametersMessage);
            RuleFor(x => x.ContactEmail).NotEmpty().When(IsEmail).WithMessage(MissingEmailMessage);
            RuleFor(x => x.HearingId).NotEmpty().When(IsHearingNotification).WithMessage(MissingHearingIdMessage);
            RuleFor(x => x.MessageType).Must(ValidMessageType).WithMessage(InvalidMessageTypeMessage);
            RuleFor(x => x.NotificationType).Must(ValidNotificationType).WithMessage(InvalidNotificationTypeMessage);
            RuleFor(x => x.ParticipantId).NotEmpty().When(IsHearingNotification).WithMessage(MissingParticipantIdMessage);
            RuleFor(x => x.PhoneNumber).NotEmpty().When(IsPhone).WithMessage(MissingPhoneNumberMessage);
        }

        private static bool ValidNotificationType(NotificationType value) => Enum.IsDefined(typeof(NotificationType), value);

        private static bool ValidMessageType(MessageType value) => Enum.IsDefined(typeof(MessageType), value);

        private static bool IsEmail(AddNotificationRequest arg) => arg.MessageType == MessageType.Email;

        private static bool IsPhone(AddNotificationRequest arg) => arg.MessageType == MessageType.SMS;

        private static bool IsHearingNotification(AddNotificationRequest arg) =>
            arg.NotificationType != NotificationType.PasswordReset;
    }
}
