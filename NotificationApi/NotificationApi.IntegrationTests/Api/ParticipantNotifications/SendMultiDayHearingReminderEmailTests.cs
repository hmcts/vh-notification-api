namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications
{
    public class SendMultiDayHearingReminderEmailTests : ApiTest
    {
        private AsyncNotificationClientStub _notifyStub;

        [SetUp]
        public void Setup()
        {
            var scope = Application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _notifyStub = scope.ServiceProvider.GetRequiredService<IAsyncNotificationClient>() as AsyncNotificationClientStub;
            _notifyStub!.SentEmails.Clear();
        }
        
        [Test]
        public async Task should_not_send_a_multi_day_reminder_email_for_a_judge()
        {
            // arrange
            var request = new MultiDayHearingReminderRequest()
            {
                RoleName = RoleNames.Judge,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.Random.Number(1,1000)}",
                CaseName = $"{Faker.Random.Number(1,1000)}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Username = $"{Guid.NewGuid()}@test.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
                TotalDays = 3
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendMultiDayHearingReminderEmail, RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeFalse(result.Content.ReadAsStringAsync().Result);
        }
        
        [Test]
        public async Task should_not_send_a_multi_day_reminder_email_for_a_judiciary_office_holder()
        {
            // arrange
            var request = new MultiDayHearingReminderRequest()
            {
                RoleName = RoleNames.JudicialOfficeHolder,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.Random.Number(1,1000)}",
                CaseName = $"{Faker.Random.Number(1,1000)}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Username = $"{Guid.NewGuid()}@test.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
                TotalDays = 3
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendMultiDayHearingReminderEmail, RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeFalse(result.Content.ReadAsStringAsync().Result);
        }
        
        [Test]
        public async Task should_send_a_multi_day_reminder_email_for_a_representative()
        {
            // arrange
            var request = new MultiDayHearingReminderRequest()
            {
                RoleName = RoleNames.Representative,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.Random.Number(1,1000)}",
                CaseName = $"{Faker.Random.Number(1,1000)}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Username = $"{Guid.NewGuid()}@test.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
                TotalDays = 3,
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendMultiDayHearingReminderEmail, RequestBody.Set(request));
            
            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId,
                request.ParticipantId, Domain.Enums.NotificationType.NewHearingReminderRepresentativeMultiDay,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_multi_day_reminder_email_for_a_lip()
        {
            // arrange
            var request = new MultiDayHearingReminderRequest()
            {
                RoleName = RoleNames.Individual,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.Random.Number(1,1000)}",
                CaseName = $"{Faker.Random.Number(1,1000)}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Username = $"{Guid.NewGuid()}@test.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
                TotalDays = 3,
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendMultiDayHearingReminderEmail, RequestBody.Set(request));
            
            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId,
                request.ParticipantId, Domain.Enums.NotificationType.NewHearingReminderLipMultiDay,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
    }
}
