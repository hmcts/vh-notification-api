namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications
{
    public class SendParticipantCreatedAccountEmailTests : ApiTest
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
        public async Task should_not_send_a_created_account_email_for_a_judge()
        {
            // arrange
            var request = new SignInDetailsEmailRequest
            {
                RoleName = RoleNames.Judge,
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Name = $"{Faker.Name.FullName()}",
                Username = $"{Guid.NewGuid()}@test.com",
                Password = $"{Faker.Random.Number(1,1000)}",
            };
        
            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantCreatedAccountEmail, RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeFalse(result.Content.ReadAsStringAsync().Result);
        }
    
        [Test]
        public async Task should_send_a_created_account_email_for_a_lip()
        {
            // arrange
            var request = new SignInDetailsEmailRequest
            {
                RoleName = RoleNames.Individual,
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Name = $"{Faker.Name.FullName()}",
                Username = $"{Guid.NewGuid()}@test.com",
                Password = $"{Faker.Random.Number(1,1000)}",
            };
        
            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantCreatedAccountEmail, RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(null, null,
                Domain.Enums.NotificationType.CreateIndividual, request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }

        [Test]
        public async Task should_send_a_created_account_email_for_a_representative()
        {
            // arrange
            var request = new SignInDetailsEmailRequest
            {
                RoleName = RoleNames.Representative,
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Name = $"{Faker.Name.FullName()}",
                Username = $"{Guid.NewGuid()}@test.com",
                Password = $"{Faker.Random.Number(1,1000)}",
            };
        
            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantCreatedAccountEmail, RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(null, null,
                Domain.Enums.NotificationType.CreateRepresentative, request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }

        [Test]
        public async Task should_send_a_created_account_email_for_a_judicial_office_holder()
        {
            // arrange
            var request = new SignInDetailsEmailRequest
            {
                RoleName = RoleNames.JudicialOfficeHolder,
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Name = $"{Faker.Name.FullName()}",
                Username = $"{Guid.NewGuid()}@test.com",
                Password = $"{Faker.Random.Number(1,1000)}",
            };
        
            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantCreatedAccountEmail, RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(null, null,
                Domain.Enums.NotificationType.CreateRepresentative, request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
    }
}
