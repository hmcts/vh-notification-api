using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.DAL;
using NotificationApi.DAL.Queries;
using NUnit.Framework;

namespace NotificationApi.IntegrationTests.Database.Queries
{
    public class DbHealthCheckQueryTests : DatabaseTestsBase
    {
        private DbHealthCheckQueryHandler _handler;
        
        [SetUp]
        public void Setup()
        {
            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            _handler = new DbHealthCheckQueryHandler(context);
        }

        [Test]
        public async Task should_return_connection_status()
        {
            var query = new DbHealthCheckQuery();
            var result = await _handler.Handle(query);
            result.CanConnect.Should().BeTrue();
        }
    }
}
