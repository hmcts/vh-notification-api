using System;
using FluentValidation;
using NotificationApi.Contract.Requests;
using NotificationApi.Extensions;

namespace NotificationApi.Validations
{
    public class NotificationCallbackRequestValidation : AbstractValidator<NotificationCallbackRequest>
    {
        public static readonly string MissingStatusMessage = "Status is required";
        public static readonly string InvalidStatusMessage = "Status is not a valid value";
        public static readonly string MissingReferenceMessage = "Reference is required";
        public static readonly string InvalidReferenceMessage = "Reference is not a valid value";
        public static readonly string MissingIdMessage = "Id is required";

        public NotificationCallbackRequestValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MissingIdMessage);
            RuleFor(x => x.Reference).NotEmpty().WithMessage(MissingReferenceMessage);
            RuleFor(x => x.Reference).Must(BeAGuid).WithMessage(InvalidReferenceMessage);
            RuleFor(x => x.Status).NotEmpty().WithMessage(MissingStatusMessage);
            RuleFor(x => x.Status).Must(BeAValidStatus).WithMessage(InvalidStatusMessage);
        }

        private bool BeAGuid(string reference)
        {
            return Guid.TryParse(reference, out _);
        }

        private bool BeAValidStatus(NotificationCallbackRequest request, string statusString)
        {
            try
            {
                request.DeliveryStatusAsEnum();
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }
    }
}
