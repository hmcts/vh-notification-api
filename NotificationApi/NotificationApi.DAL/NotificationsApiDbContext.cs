using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationApi.Domain;

namespace NotificationApi.DAL
{
    public class NotificationsApiDbContext : DbContext
    {
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Template> Templates { get; set; }

        public NotificationsApiDbContext(DbContextOptions options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var applyGenericMethods =
                typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public |
                                                BindingFlags.FlattenHierarchy);
            var applyGenericApplyConfigurationMethods = applyGenericMethods.Where(m =>
                m.IsGenericMethod && m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));
            var applyGenericMethod = applyGenericApplyConfigurationMethods.First(m =>
                m.GetParameters().FirstOrDefault()?.ParameterType.Name == "IEntityTypeConfiguration`1");

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters))
            {
                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType &&
                        iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {

                        var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        applyConcreteMethod.Invoke(modelBuilder, new[] {Activator.CreateInstance(type)});
                        break;
                    }
                }
            }
        }

        public override int SaveChanges()
        {
            SetUpdatedDateValue();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetUpdatedDateValue();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetUpdatedDateValue()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified))
            {
                var updatedDateProperty =
                    entry.Properties.AsQueryable().FirstOrDefault(x => x.Metadata.Name == "UpdatedAt");
                if (updatedDateProperty != null)
                {
                    updatedDateProperty.CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}
