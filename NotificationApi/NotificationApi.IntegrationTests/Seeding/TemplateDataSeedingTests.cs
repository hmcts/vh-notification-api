using Microsoft.EntityFrameworkCore;
using NotificationApi.Contract;
using NotificationApi.DAL;
using NotificationApi.Domain;
using MessageType = NotificationApi.Domain.Enums.MessageType;

namespace NotificationApi.IntegrationTests.Seeding
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
            dbContextOptionsBuilder.UseInMemoryDatabase("InMemoryDbForTesting");
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
            var templateDataForEnvironments = new TemplateDataForEnvironments();
            var preProdTemplates = templateDataForEnvironments.Get("PreProd");
            var expectedTotalTemplates = Enum.GetNames(typeof(NotificationType)).Length;

            // imitate a database restore from another env
            _dbContext.Templates.AddRange(preProdTemplates);
            // imitate a duplicate of the same template
            var clone1 = new Template(Guid.NewGuid(), Domain.Enums.NotificationType.CreateIndividual, MessageType.Email,
                "test");
            var clone2 = new Template(new Guid("94D06843-4608-4CDA-9933-9D0F3D7CE535"), Domain.Enums.NotificationType.CreateIndividual, MessageType.Email,
                "test");
            _dbContext.Templates.Add(clone1);
            _dbContext.Templates.Add(clone2);
            
            _dbContext.SaveChanges();
            
            _sut.Run("Dev");

            _dbContext.Templates.Count().Should().Be(expectedTotalTemplates);
        }
    }
}
