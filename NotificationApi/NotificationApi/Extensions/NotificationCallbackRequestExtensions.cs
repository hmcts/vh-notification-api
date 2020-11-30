using System;
using NotificationApi.Contract.Requests;
using NotificationApi.Domain.Enums;

namespace NotificationApi.Extensions
{
    public static class NotificationCallbackRequestExtensions
    {
        public static Guid ReferenceAsGuid(this NotificationCallbackRequest request)
        {
            return Guid.Parse(request.Reference);
        }
        
        public static DeliveryStatus DeliveryStatusAsEnum(this NotificationCallbackRequest request)
        {
            return request.Status switch
            {
                "delivered" => DeliveryStatus.Delivered,
                "permanent-failure" => DeliveryStatus.Failed,
                "temporary-failure" => DeliveryStatus.Failed,
                "technical-failure" => DeliveryStatus.Failed,
                _ => throw new ArgumentOutOfRangeException($"{request.Status} is not a recognised status")
            };
        }
    }
}
