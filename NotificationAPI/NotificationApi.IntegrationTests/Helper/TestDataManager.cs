using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationApi.Common.Configuration;
using NotificationApi.DAL;

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

        public Task<Domain.Notification> SeedNotification()
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Notification> SeedConference(Domain.Notification notification)
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
