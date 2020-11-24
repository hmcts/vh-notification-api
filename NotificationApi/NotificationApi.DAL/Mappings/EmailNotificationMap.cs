using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationApi.Domain;

namespace NotificationApi.DAL.Mappings
{
    public class EmailNotificationMap : IEntityTypeConfiguration<EmailNotification>
    {
        public void Configure(EntityTypeBuilder<EmailNotification> builder)
        {
            builder.Property(x => x.ToEmail).IsRequired();
        }
    }
}
