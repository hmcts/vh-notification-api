
namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications
{
    public class SendSingleDayHearingReminderEmailTests : ApiTest
    {
        private AsyncNotificationClientStub _notifyStub;

        [SetUp]
        public void Setup()
        {
            var scope = Application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _notifyStub =
                scope.ServiceProvider.GetRequiredService<IAsyncNotificationClient>() as AsyncNotificationClientStub;
            _notifyStub!.SentEmails.Clear();
        }

        [Test]
        public async Task should_not_send_a_single_day_reminder_email_for_a_judge()
        {
            // arrange
            var request = new SingleDayHearingReminderRequest()
            {
                RoleName = RoleNames.Judge,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Username = $"{Guid.NewGuid()}@test.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1)
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendSingleDayHearingReminderEmail,
                RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeFalse(result.Content.ReadAsStringAsync().Result);
        }

        [Test]
        public async Task should_send_a_single_day_reminder_email_for_a_judicial_office_holder()
        {
            // arrange
            var request = new SingleDayHearingReminderRequest()
            {
                RoleName = RoleNames.JudicialOfficeHolder,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Username = $"{Guid.NewGuid()}@test.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1)
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendSingleDayHearingReminderEmail,
                RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId,
                request.ParticipantId, Domain.Enums.NotificationType.NewHearingReminderJOH,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail
                                               && x.ExternalRefId == notifications[0].ExternalId
            ).Should().BeTrue();
        }

        [Test]
        public async Task should_send_a_single_day_reminder_email_for_a_judiciary_judicial_office_holder()
        {
            // arrange
            var request = new SingleDayHearingReminderRequest()
            {
                RoleName = RoleNames.JudicialOfficeHolder,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@judiciary.com",
                Username = $"{Guid.NewGuid()}@judiciary.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1)
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendSingleDayHearingReminderEmail,
                RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId,
                request.ParticipantId, Domain.Enums.NotificationType.NewHearingReminderEJudJoh,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail
                                               && x.ExternalRefId == notifications[0].ExternalId
            ).Should().BeTrue();
        }

        [Test]
        public async Task should_send_a_single_day_reminder_email_for_a_representative()
        {
            // arrange
            var request = new SingleDayHearingReminderRequest()
            {
                RoleName = RoleNames.Representative,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@judiciary.com",
                Username = $"{Guid.NewGuid()}@judiciary.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
                Representee = "John Doe"
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendSingleDayHearingReminderEmail,
                RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId,
                request.ParticipantId, Domain.Enums.NotificationType.NewHearingReminderRepresentativeSingleDay,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail
                                               && x.ExternalRefId == notifications[0].ExternalId
            ).Should().BeTrue();
        }

        [Test]
        public async Task should_send_a_single_day_reminder_email_for_a_lip_toggle_on()
        {
            // arrange
            var request = new SingleDayHearingReminderRequest()
            {
                RoleName = RoleNames.Individual,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@judiciary.com",
                Username = $"{Guid.NewGuid()}@judiciary.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1)
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendSingleDayHearingReminderEmail,
                RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId,
                request.ParticipantId, Domain.Enums.NotificationType.NewHearingReminderLipSingleDay,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail
                                               && x.ExternalRefId == notifications[0].ExternalId
            ).Should().BeTrue();
        }
    }
}
