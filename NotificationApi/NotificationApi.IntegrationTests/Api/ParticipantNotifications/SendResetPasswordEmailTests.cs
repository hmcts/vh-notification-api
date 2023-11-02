namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications
{
    public class SendResetPasswordEmailTests : ApiTest
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
        public async Task should_return_bad_request_when_validation_fails()
        {
            // arrange
            var request = new PasswordResetEmailRequest()
            {
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Name = $"{Faker.Name.FullName()}",
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantPasswordResetEmail, RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeFalse();

            // assert
            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var validationProblemDetails = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
            validationProblemDetails.Errors[$"{nameof(request.Password)}"].ToList()
                .Exists(errorMessage => errorMessage.Contains("must not be empty")).Should().BeTrue();
            
            _notifyStub.SentEmails.Count.Should().Be(0);
        }

        [Test]
        public async Task should_send_a_password_reset_email()
        {
            // arrange
            var request = new PasswordResetEmailRequest()
            {
                ContactEmail = $"{Guid.NewGuid()}@test.com",
                Name = $"{Faker.Name.FullName()}",
                Password = $"{Faker.RandomNumber.Next()}",
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantPasswordResetEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue();
            
            var notifications = await TestDataManager.GetNotifications(null, null,
                Domain.Enums.NotificationType.PasswordReset, request.ContactEmail);
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
        }
    }
}
