
namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications
{
    public class SendParticipantMultiDayHearingConfirmationForExistingUserEmailTests : ApiTest
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
        public async Task should_send_a_multi_day_confirmation_email_for_a_non_judiciary_judicial_office_holder()
        {
            // arrange
            var request = new ExistingUserMultiDayHearingConfirmationRequest
            {
                RoleName = RoleNames.JudicialOfficeHolder,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com",
                Username = $"{Guid.NewGuid()}@intautomation.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
                TotalDays = 3
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantMultiDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value,
                request.ParticipantId.Value, Domain.Enums.NotificationType.HearingConfirmationJohMultiDay,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_multi_day_confirmation_email_for_a_judiciary_judicial_office_holder()
        {
            // arrange
            var request = new ExistingUserMultiDayHearingConfirmationRequest
            {
                RoleName = RoleNames.JudicialOfficeHolder,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@judiciary.com",
                Username = $"{Guid.NewGuid()}@judiciary.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
                TotalDays = 3
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantMultiDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value,
                request.ParticipantId.Value, Domain.Enums.NotificationType.HearingConfirmationEJudJohMultiDay,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_multi_day_confirmation_email_for_a_judiciary_judge()
        {
            // arrange
            var request = new ExistingUserMultiDayHearingConfirmationRequest
            {
                RoleName = RoleNames.Judge,
                Name = $"{Faker.Name.FullName()}",
                DisplayName = "Judge Fudge",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@judiciary.com",
                Username = $"{Guid.NewGuid()}@judiciary.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
                TotalDays = 3
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantMultiDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value,
                request.ParticipantId.Value, Domain.Enums.NotificationType.HearingConfirmationEJudJudgeMultiDay,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_multi_day_confirmation_email_for_a_representative()
        {
            // arrange
            var request = new ExistingUserMultiDayHearingConfirmationRequest
            {
                RoleName = RoleNames.Representative,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com",
                Username = $"{Guid.NewGuid()}@intautomation.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
                Representee = $"{Faker.Name.FullName()}",
                TotalDays = 3
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantMultiDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);
  
            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value,
                request.ParticipantId.Value, Domain.Enums.NotificationType.ExistingUserRepresentativeConfirmationMultiDay,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_multi_day_confirmation_email_for_a_lip()
        {
            // arrange
            var request = new ExistingUserMultiDayHearingConfirmationRequest
            {
                RoleName = RoleNames.Individual,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com",
                Username = $"{Guid.NewGuid()}@intautomation.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
                TotalDays = 3
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantMultiDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);
  
            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
                Domain.Enums.NotificationType.ExistingUserLipConfirmationMultiDay,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
    }
}
