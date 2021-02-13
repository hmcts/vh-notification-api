using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Mappings
{
    public class NotificationMap : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable(nameof(Notification));
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Payload);
            builder.Property(x => x.DeliveryStatus).IsRequired().HasDefaultValue(DeliveryStatus.NotSent);
            builder.Property(x => x.NotificationType).IsRequired();
            builder.Property(x => x.ParticipantRefId).IsRequired(false);
            builder.Property(x => x.HearingRefId).IsRequired(false);
            builder.Property(x => x.ExternalId);
        }
    }
}
