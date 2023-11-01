namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications;

public class SendParticipantSingleDayHearingConfirmationForNewUserEmailTests : ApiTest
{
    private AsyncNotificationClientStub _notifyStub;

    [SetUp]
    public void Setup()
    {
        var scope = Application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _notifyStub = scope.ServiceProvider.GetRequiredService<IAsyncNotificationClient>() as AsyncNotificationClientStub;
        _notifyStub!.SentEmails.Clear();
    }
    
    [TestCase(RoleNames.Representative)]
    [TestCase(RoleNames.Judge)]
    [TestCase(RoleNames.PanelMember)]
    [TestCase(RoleNames.JudicialOfficeHolder)]
    [TestCase(RoleNames.Winger)]
    public async Task should_not_send_a_confirmation_email_for_a(string roleName)
    {
        // arrange
        var request = new NewUserSingleDayHearingConfirmationRequest
        {
            RoleName = roleName
        };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForNewUserEmail, RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var validationProblemDetails = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
        validationProblemDetails.Errors[$"{nameof(request.CaseName)}"].ToList()
            .Exists(errorMessage => errorMessage.Contains("must not be empty")).Should().BeTrue();
        validationProblemDetails.Errors[$"{nameof(request.RoleName)}"].Should().Contain(NewUserSingleDayHearingConfirmationRequestValidation.UnsupportedRoleMessage);
            
        _notifyStub.SentEmails.Count.Should().Be(0);
    }
        
    [Test]
    public async Task should_send_a_confirmation_email_for_a_lip()
    {
        // arrange
        var request = new NewUserSingleDayHearingConfirmationRequest
        {
            RoleName = RoleNames.Individual,
            Name = $"{Faker.Name.FullName()}",
            CaseNumber = $"{Faker.RandomNumber.Next()}",
            CaseName = $"{Faker.RandomNumber.Next()}",
            HearingId = Guid.NewGuid(),
            ParticipantId = Guid.NewGuid(),
            ContactEmail = $"{Guid.NewGuid()}@intautomation.com",
            Username = $"{Guid.NewGuid()}@intautomation.com",
            RandomPassword = "12345678dusausyd",
            ScheduledDateTime = DateTime.UtcNow.AddDays(1)
        };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantSingleDayHearingConfirmationForNewUserEmail, RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);
  
        var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
            (Domain.Enums.NotificationType)  Contract.NotificationType.NewUserLipConfirmation,
            request.ContactEmail);
        notifications.Count.Should().Be(1);
        _notifyStub.SentEmails.Count.Should().Be(1);
        _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                           && x.ExternalRefId == notifications[0].ExternalId 
        ).Should().BeTrue();
    }
}
