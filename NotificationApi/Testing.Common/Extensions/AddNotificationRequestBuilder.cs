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
                ContactEmail = messageType == MessageType.Email ? "email@hmcts.net" : null,
                HearingId = Guid.NewGuid(),
                MessageType = messageType,
                NotificationType = notificationType,
                Parameters = parameters,
                ParticipantId = Guid.NewGuid(),
                PhoneNumber = messageType == MessageType.SMS ? "01234567890" : null,
            };
        }
        
        public static AddNotificationRequest BuildNonHearingRequest(MessageType messageType, NotificationType notificationType,
            Dictionary<string, string> parameters)
        {
            return new AddNotificationRequest
            {
                ContactEmail = messageType == MessageType.Email ? "email@hmcts.net" : null,
                MessageType = messageType,
                NotificationType = notificationType,
                Parameters = parameters,
                HearingId = null,
                ParticipantId = null,
                PhoneNumber = messageType == MessageType.SMS ? "01234567890" : null,
            };
        }
    }
}
