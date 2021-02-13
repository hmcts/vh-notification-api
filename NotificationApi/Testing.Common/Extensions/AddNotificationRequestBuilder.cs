using System;
using System.Collections.Generic;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;

namespace Testing.Common.Extensions
{
    public static class AddNotificationRequestBuilder
    {
        public static AddNotificationRequest BuildRequest(MessageType messageType, NotificationType notificationType,
            Dictionary<string, string> parameters)
        {
            return new AddNotificationRequest
            {
                ContactEmail = messageType == MessageType.Email ? "email@email.com" : null,
                HearingId = Guid.NewGuid(),
                MessageType = messageType,
                NotificationType = notificationType,
                Parameters = parameters,
                ParticipantId = Guid.NewGuid(),
                PhoneNumber = messageType == MessageType.SMS ? "01234567890" : null,
            };
        }
    }
}
