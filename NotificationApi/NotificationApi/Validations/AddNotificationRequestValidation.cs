using System.Linq;
using FluentValidation;
using NotificationApi.Contract.Requests;

namespace NotificationApi.Validations
{
    public class AddNotificationRequestValidation : AbstractValidator<AddNotificationRequest>
    {
        public static readonly string MissingParametersMessage = "Parameters are required";
        public static readonly string MissingEmailMessage = "Email is required";
        public static readonly string MissingHearingIdMessage = "HearingId is required";
        public static readonly string InvalidMessageTypeMessage = "Message type is invalid";
        public static readonly string InvalidNotificationTypeMessage = "Notification type is invalid";
        public static readonly string MissingParticipantIdMessage = "Participant is required";
        public static readonly string MissingPhoneNumberMessage = "Phone number is required";
        
        public AddNotificationRequestValidation()
        {
            RuleFor(x => x.Parameters).NotEmpty().WithMessage(MissingParametersMessage);
            RuleFor(x => x.ContactEmail).NotEmpty().WithMessage(MissingEmailMessage);
            RuleFor(x => x.HearingId).NotEmpty().WithMessage(MissingHearingIdMessage);
            RuleFor(x => x.MessageType).Must(ValidMessageType).WithMessage(InvalidMessageTypeMessage);
            RuleFor(x => x.NotificationType).Must(ValidNotificationType).WithMessage(InvalidNotificationTypeMessage);
            RuleFor(x => x.ParticipantId).NotEmpty().WithMessage(MissingParticipantIdMessage);
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(MissingPhoneNumberMessage);
        }

        private bool ValidNotificationType(int arg)
        {
            var availableNotifications = new[]
            {
                1 //Create individual
            };

            return availableNotifications.Contains(arg);
        }

        private bool ValidMessageType(int arg)
        {
            var availableMessages = new[]
            {
                1, // Email
                2 // SMS
            };
            
            return availableMessages.Contains(arg);
        }
    }
}
