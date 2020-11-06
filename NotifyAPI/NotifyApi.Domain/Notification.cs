using System;
using NotifyApi.Domain.Ddd;
using NotifyApi.Domain.Enums;

namespace NotifyApi.Domain
{
    public abstract class Notification : Entity<Guid>
    {
        public abstract MessageType MessageType { get; }
        public string Payload { get; }
        public DeliveryStatus DeliveryStatus { get; private set; }
        public NotificationType NotificationType { get; }
        public Guid ParticipantRefId { get; }
        public Guid HearingRefId { get; }

        protected Notification()
        {
            Id = Guid.NewGuid();
            DeliveryStatus = DeliveryStatus.NotSent;
        }

        protected Notification(NotificationType notificationType, string payload,
            Guid participantRefId, Guid hearingRefId) : this()
        {
            NotificationType = notificationType;
            Payload = payload;
            ParticipantRefId = participantRefId;
            HearingRefId = hearingRefId;
        }

        public void UpdateDeliveryStatus(DeliveryStatus newStatus)
        {
            DeliveryStatus = newStatus;
        }
    }
}
