using System;
using NotificationApi.Domain.Enums;

namespace NotificationApi.Domain
{
    public abstract class Notification : TrackableEntity<Guid>
    {
        public abstract MessageType MessageType { get; }
        public string Payload { get; private set; }
        public DeliveryStatus DeliveryStatus { get; private set; }
        public NotificationType NotificationType { get; }
        public Guid? ParticipantRefId { get; }
        public Guid? HearingRefId { get; }
        public string ExternalId { get; private set; }
        public string Parameters { get; set; }
        protected Notification(Guid id)
        {
            Id = id;
            DeliveryStatus = DeliveryStatus.NotSent;
        }

        protected Notification()
        {
            Id = Guid.NewGuid();
            DeliveryStatus = DeliveryStatus.NotSent;
        }

        protected Notification(Guid id, NotificationType notificationType,
            Guid? participantRefId, Guid? hearingRefId) : this(id)
        {
            NotificationType = notificationType;
            ParticipantRefId = participantRefId;
            HearingRefId = hearingRefId;
        }

        public void AssignExternalId(string externalNotificationId)
        {
            ExternalId = externalNotificationId;
        }

        public void UpdateDeliveryStatus(DeliveryStatus newStatus)
        {
            DeliveryStatus = newStatus;
        }

        public void AssignPayload(string payload)
        {
            Payload = payload;
        }
    }
}
