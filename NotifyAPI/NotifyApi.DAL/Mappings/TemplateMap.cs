using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotifyApi.Domain;

namespace NotifyApi.DAL.Mappings
{
    public class TemplateMap : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable(nameof(Template));
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NotifyTemplateId).IsRequired();
            builder.Property(x => x.NotificationType).IsRequired();
            builder.Property(x => x.MessageType).IsRequired();
            builder.Property(x => x.Parameters).IsRequired();
        }
    }
}
