using System;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NotificationApi.Contract;
using NotificationApi.DAL;
using NUnit.Framework;

namespace NotificationApi.UnitTests.Seeding
{

    [TestFixture]
    public class TemplateDataSeedingTests
    {
        private NotificationsApiDbContext _dbContext;
        private TemplateDataSeeding _sut;

        [SetUp]
        public void Setup()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<NotificationsApiDbContext>();
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            dbContextOptionsBuilder.UseInMemoryDatabase("VhNotificationsApi");
            var notifyBookingsDbContextOptions = dbContextOptionsBuilder.Options;
            _dbContext = new NotificationsApiDbContext(notifyBookingsDbContextOptions);
            _sut = new TemplateDataSeeding(_dbContext);
        }

        [Test]
        public void should_add_templates_for_an_environment()
        {
            var environment = "Dev";
            var expectedTotalTemplates = Enum.GetNames(typeof(NotificationType)).Length;

            _sut.Run(environment);

            _dbContext.Templates.Count().Should().Be(expectedTotalTemplates);
        }

        [Test]
        public void should_remove_templates_where_id_do_not_match()
        {
            var oldEnvironment = "PreProd";
            var newEnvironment = "Dev";
            var expectedTotalTemplates = Enum.GetNames(typeof(NotificationType)).Length;

            _sut.Run(oldEnvironment);
            _sut.Run(newEnvironment);

            _dbContext.Templates.Count().Should().Be(expectedTotalTemplates);
        }
    }
}
