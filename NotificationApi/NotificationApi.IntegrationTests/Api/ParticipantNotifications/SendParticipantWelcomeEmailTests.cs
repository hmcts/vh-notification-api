namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications
{
    public class SendParticipantWelcomeEmailTests : ApiTest
    {
        private AsyncNotificationClientStub _notifyStub;

        [SetUp]
        public void Setup()
        {
            var scope = Application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _notifyStub = scope.ServiceProvider.GetRequiredService<IAsyncNotificationClient>() as AsyncNotificationClientStub;
            _notifyStub!.SentEmails.Clear();
        }
        
        [TestCase(RoleNames.Judge)]
        public async Task should_not_send_a_welcome_email_for_a(string roleName)
        {
            // arrange
            var request = new NewUserWelcomeEmailRequest
            {
                RoleName = roleName,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com"
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantWelcomeEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var validationProblemDetails = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
            validationProblemDetails.Errors[$"request"].ToList()
                .Exists(errorMessage => errorMessage.Contains("Role is not supported")).Should().BeTrue();
            
            _notifyStub.SentEmails.Count.Should().Be(0);
        }

        [Test]
        public async Task should_send_a_welcome_email_for_a_lip()
        {
            // arrange
            var request = new NewUserWelcomeEmailRequest
            {
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                RoleName = RoleNames.Individual,
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com"
            };
            
            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantWelcomeEmail, RequestBody.Set(request));

            
            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
                Domain.Enums.NotificationType.NewUserLipWelcome,
                request.ContactEmail);
            
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
            
        }
        
        [Test]
        public async Task should_send_a_welcome_email_for_a_representative()
        {
            // arrange
            var request = new NewUserWelcomeEmailRequest
            {
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                RoleName = RoleNames.Representative,
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com"
            };
            
            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantWelcomeEmail, RequestBody.Set(request));

            
            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
                Domain.Enums.NotificationType.NewUserRepresentativeWelcome,
                request.ContactEmail);
            
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
            
        }
        
        [Test]
        public async Task should_send_a_welcome_email_for_a_panel_member()
        {
            // arrange
            var request = new NewUserWelcomeEmailRequest
            {
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                RoleName = RoleNames.JudicialOfficeHolder,
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com"
            };
            
            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantWelcomeEmail, RequestBody.Set(request));

            
            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
                Domain.Enums.NotificationType.NewUserRepresentativeWelcome,
                request.ContactEmail);
            
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
            
        }
    }
}
