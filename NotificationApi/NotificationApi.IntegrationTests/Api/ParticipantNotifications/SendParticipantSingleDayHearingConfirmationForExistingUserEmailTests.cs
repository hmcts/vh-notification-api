using NotificationApi.Common.Util;
using Testing.Common.Stubs;

namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications
{
    public class SendParticipantSingleDayHearingConfirmationForExistingUserEmailTests : ApiTest
    {
        private AsyncNotificationClientStub _notifyStub;
        private FeatureTogglesStub _featureToggleStub;

        [SetUp]
        public void Setup()
        {
            var scope = Application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _notifyStub = scope.ServiceProvider.GetRequiredService<IAsyncNotificationClient>() as AsyncNotificationClientStub;
            _notifyStub!.SentEmails.Clear();
            _featureToggleStub = Application.Services.GetService(typeof(IFeatureToggles)) as FeatureTogglesStub;
            _featureToggleStub!.UseNew2023Templates = false;
        }

        [Test]
        public async Task should_return_bad_request_when_validation_fails()
        {
            // arrange
            var request = new ExistingUserSingleDayHearingConfirmationRequest
            {
                RoleName = "MadeUp"
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeFalse(result.Content.ReadAsStringAsync().Result);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var validationProblemDetails = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
            validationProblemDetails.Errors.Should().ContainKey(nameof(request.Name));
            validationProblemDetails.Errors.Should().ContainKey(nameof(request.CaseName));
            validationProblemDetails.Errors.Should().ContainKey(nameof(request.CaseNumber));
        }
        
        [Test]
        public async Task should_send_a_confirmation_email_for_a_non_judiciary_judicial_office_holder()
        {
            // arrange
            var request = new ExistingUserSingleDayHearingConfirmationRequest
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
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value,
                request.ParticipantId.Value, Domain.Enums.NotificationType.HearingConfirmationJoh,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_confirmation_email_for_a_judiciary_judicial_office_holder()
        {
            // arrange
            var request = new ExistingUserSingleDayHearingConfirmationRequest
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
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value,
                request.ParticipantId.Value, Domain.Enums.NotificationType.HearingConfirmationEJudJoh,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_confirmation_email_for_a_non_judiciary_judge()
        {
            // arrange
            var request = new ExistingUserSingleDayHearingConfirmationRequest
            {
                RoleName = RoleNames.Judge,
                Name = $"{Faker.Name.FullName()}",
                DisplayName = "Judge Fudge",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com",
                Username = $"{Guid.NewGuid()}@intautomation.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value,
                request.ParticipantId.Value, Domain.Enums.NotificationType.HearingConfirmationJudge,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_confirmation_email_for_a_judiciary_judge()
        {
            // arrange
            var request = new ExistingUserSingleDayHearingConfirmationRequest
            {
                RoleName = RoleNames.Judge,
                DisplayName = "Judge Fudge",
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@judiciary.com",
                Username = $"{Guid.NewGuid()}@judiciary.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1),
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value,
                request.ParticipantId.Value, Domain.Enums.NotificationType.HearingConfirmationEJudJudge,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_confirmation_email_for_a_representative_toggle_off()
        {
            // arrange
            _featureToggleStub!.UseNew2023Templates = false;
            var request = new ExistingUserSingleDayHearingConfirmationRequest
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
                Representee = $"{Faker.Name.FullName()}"
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);
  
            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value,
                request.ParticipantId.Value, Domain.Enums.NotificationType.HearingConfirmationRepresentative,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_confirmation_email_for_a_representative_toggle_on()
        {
            // arrange
            _featureToggleStub!.UseNew2023Templates = true;
            var request = new ExistingUserSingleDayHearingConfirmationRequest
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
                Representee = $"{Faker.Name.FullName()}"
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);
  
            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value,
                request.ParticipantId.Value, Domain.Enums.NotificationType.ExistingUserRepresentativeConfirmation,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_confirmation_email_for_a_lip_feature_toggle_off()
        {
            // arrange
            _featureToggleStub!.UseNew2023Templates = false;
            var request = new ExistingUserSingleDayHearingConfirmationRequest
            {
                RoleName = RoleNames.Individual,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com",
                Username = $"{Guid.NewGuid()}@intautomation.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1)
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);
  
            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
                Domain.Enums.NotificationType.HearingConfirmationLip,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
        
        [Test]
        public async Task should_send_a_confirmation_email_for_a_lip_feature_toggle_on()
        {
            // arrange
            _featureToggleStub!.UseNew2023Templates = true;
            var request = new ExistingUserSingleDayHearingConfirmationRequest
            {
                RoleName = RoleNames.Individual,
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com",
                Username = $"{Guid.NewGuid()}@intautomation.com",
                ScheduledDateTime = DateTime.UtcNow.AddDays(1)
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForExistingUserEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);
  
            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
                Domain.Enums.NotificationType.ExistingUserLipConfirmation,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
    }
}
