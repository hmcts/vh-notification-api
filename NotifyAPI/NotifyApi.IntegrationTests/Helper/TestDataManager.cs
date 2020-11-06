using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotifyApi.Common.Configuration;
using NotifyApi.DAL;
using NotifyApi.Domain;

namespace NotifyApi.IntegrationTests.Helper
{
    public class TestDataManager
    {
        private readonly ServicesConfiguration _services;
        private readonly DbContextOptions<NotifyApiDbContext> _dbContextOptions;

        public TestDataManager(ServicesConfiguration services, DbContextOptions<NotifyApiDbContext> dbContextOptions)
        {
            _services = services;
            _dbContextOptions = dbContextOptions;
        }

        public Task<Notification> SeedNotification()
        {
            throw new NotImplementedException();
        }

        public async Task<Notification> SeedConference(Notification notification)
        {
            await using var db = new NotifyApiDbContext(_dbContextOptions);
            await db.Notifications.AddAsync(notification);
            await db.SaveChangesAsync();

            return notification;
        }

        public async Task RemoveNotification(Guid notificationId)
        {
            await using var db = new NotifyApiDbContext(_dbContextOptions);
            var notification = await db.Notifications.SingleAsync(x => x.Id == notificationId);

            db.Remove(notification);
            await db.SaveChangesAsync();
        }
        
        public async Task RemoveNotifications(IEnumerable<Guid> notificationIds)
        {
            await using var db = new NotifyApiDbContext(_dbContextOptions);
            var notifications = await db.Notifications.Where(x => notificationIds.Contains(x.Id)).ToListAsync();

            db.RemoveRange(notifications);
            await db.SaveChangesAsync();
        }
    }
}
