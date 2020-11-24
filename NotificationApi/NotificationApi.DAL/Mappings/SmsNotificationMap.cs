using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationApi.Domain;

namespace NotificationApi.DAL.Mappings
{
    public class SmsNotificationMap : IEntityTypeConfiguration<SmsNotification>
    {
        public void Configure(EntityTypeBuilder<SmsNotification> builder)
        {
            builder.Property(x => x.PhoneNumber).IsRequired();
        }
    }
}
