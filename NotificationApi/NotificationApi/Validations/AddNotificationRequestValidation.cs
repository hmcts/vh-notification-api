using System;
using FluentValidation;
using NotificationApi.Contract.Requests;
using NotificationApi.Domain.Enums;

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
            RuleFor(x => x.ContactEmail).NotEmpty().When(IsEmail).WithMessage(MissingEmailMessage);
            RuleFor(x => x.HearingId).NotEmpty().WithMessage(MissingHearingIdMessage);
            RuleFor(x => x.MessageType).Must(ValidMessageType).WithMessage(InvalidMessageTypeMessage);
            RuleFor(x => x.NotificationType).Must(ValidNotificationType).WithMessage(InvalidNotificationTypeMessage);
            RuleFor(x => x.ParticipantId).NotEmpty().WithMessage(MissingParticipantIdMessage);
            RuleFor(x => x.PhoneNumber).NotEmpty().When(IsPhone).WithMessage(MissingPhoneNumberMessage);
        }

        private bool ValidNotificationType(int value) => Enum.IsDefined(typeof(NotificationType), value);

        private bool ValidMessageType(int value) => Enum.IsDefined(typeof(MessageType), value);

        private bool IsEmail(AddNotificationRequest arg)
        {
            return arg.MessageType == (int) MessageType.Email;
        }
        
        private bool IsPhone(AddNotificationRequest arg)
        {
            return arg.MessageType == (int) MessageType.SMS;
        }
    }
}
