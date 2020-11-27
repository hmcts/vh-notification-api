using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationApi.Common.Configuration;
using NotificationApi.DAL;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.IntegrationTests.Helper
{
    public class TestDataManager
    {
        private readonly ServicesConfiguration _services;
        private readonly DbContextOptions<NotificationsApiDbContext> _dbContextOptions;

        public TestDataManager(ServicesConfiguration services, DbContextOptions<NotificationsApiDbContext> dbContextOptions)
        {
            _services = services;
            _dbContextOptions = dbContextOptions;
        }

        public Task<Notification> SeedNotification()
        {
            var notification = new EmailNotification(NotificationType.CreateUser, "totest@auto.com", Guid.NewGuid(),
                Guid.NewGuid());
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
