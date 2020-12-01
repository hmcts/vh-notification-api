using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.IntegrationTests.Helper
{
    public class TestDataManager
    {
        private readonly DbContextOptions<NotificationsApiDbContext> _dbContextOptions;

        public TestDataManager(DbContextOptions<NotificationsApiDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        
        public Task<Notification> SeedCreatedNotification()
        {
            var notification = new EmailNotification(Guid.NewGuid(), NotificationType.CreateIndividual, "totest@auto.com", Guid.NewGuid(),
                Guid.NewGuid());
            return SeedNotification(notification);
        }

        public Task<Notification> SeedSendingNotification()
        {
            var notification = new EmailNotification(Guid.NewGuid(), NotificationType.CreateIndividual, "totest@auto.com", Guid.NewGuid(),
                Guid.NewGuid());
            notification.UpdateDeliveryStatus(DeliveryStatus.Sending);
            notification.AssignExternalId(Guid.NewGuid().ToString());
            return SeedNotification(notification);
        }

        public async Task<Notification> SeedNotification(Notification notification)
        {
            await using var db = new NotificationsApiDbContext(_dbContextOptions);
            await db.Notifications.AddAsync(notification);
            await db.SaveChangesAsync();

            return notification;
        }

        public async Task RemoveNotification(Guid notificationId)
        {
            await using var db = new NotificationsApiDbContext(_dbContextOptions);
            var notification = await db.Notifications.SingleAsync(x => x.Id == notificationId);

            db.Remove(notification);
            await db.SaveChangesAsync();
        }
        
        public async Task RemoveNotifications(IEnumerable<Guid> notificationIds)
        {
            await using var db = new NotificationsApiDbContext(_dbContextOptions);
            var notifications = await db.Notifications.Where(x => notificationIds.Contains(x.Id)).ToListAsync();

            db.RemoveRange(notifications);
            await db.SaveChangesAsync();
        }
    }
}
