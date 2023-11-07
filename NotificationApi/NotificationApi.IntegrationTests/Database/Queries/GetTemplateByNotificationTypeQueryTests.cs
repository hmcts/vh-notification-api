using NotificationApi.DAL;
using NotificationApi.DAL.Exceptions;
using NotificationApi.DAL.Queries;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.IntegrationTests.Database.Queries
{
    public class GetTemplateByNotificationTypeQueryTests : DatabaseTestsBase
    {
        private GetTemplateByNotificationTypeQueryHandler _handler;
        private Template _duplicateTemplate;

        [SetUp]
        public void Setup()
        {
            _duplicateTemplate = null;
            var context = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            _handler = new GetTemplateByNotificationTypeQueryHandler(context);
        }

        [TearDown]
        public async Task TearDown()
        {
            if (_duplicateTemplate == null) return;
            TestContext.WriteLine($"Removing duplicate template {_duplicateTemplate.Id}");
            await using var db = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            db.Templates.Remove(_duplicateTemplate);
            await db.SaveChangesAsync();
        }

        [TestCase(NotificationType.CreateIndividual)]
        [TestCase(NotificationType.CreateRepresentative)]
        [TestCase(NotificationType.PasswordReset)]
        public async Task should_get_template_for_notification_type(NotificationType notificationType)
        {
            var query = new GetTemplateByNotificationTypeQuery(notificationType);
            var result = await _handler.Handle(query);
            result.Should().NotBeNull();
        }

        [Test]
        public async Task should_throw_exception_when_duplicate_templates_are_found()
        {
            // create and add duplicate template
            _duplicateTemplate = new Template(Guid.Parse("04a2dd3d-06ba-462b-a866-7fc802aae69a"),
                NotificationType.HearingReminderLip, MessageType.Email,
                "case name,case number,name,day month year,time,username");

            await using var db = new NotificationsApiDbContext(NotifyBookingsDbContextOptions);
            await db.Templates.AddAsync(_duplicateTemplate);
            await db.SaveChangesAsync();


            var query = new GetTemplateByNotificationTypeQuery(_duplicateTemplate.NotificationType);

            var f = async () => { await _handler.Handle(query); };
            await f.Should().ThrowAsync<DuplicateNotificationTemplateException>();

        }
    }
}
