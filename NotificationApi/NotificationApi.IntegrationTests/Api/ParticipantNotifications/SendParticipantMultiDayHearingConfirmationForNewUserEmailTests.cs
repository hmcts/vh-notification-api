namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications;

public class SendParticipantMultiDayHearingConfirmationForNewUserEmailTests : ApiTest
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
    [TestCase(RoleNames.JudicialOfficeHolder)]
    public async Task should_not_send_a_confirmation_email_for_a(string roleName)
    {
        // arrange
        var request = new NewUserMultiDayHearingConfirmationRequest()
        {
            RoleName = roleName,
            Name = $"{Faker.Name.FullName()}",
            CaseNumber = $"{Faker.RandomNumber.Next()}",
            CaseName = $"{Faker.RandomNumber.Next()}",
            HearingId = Guid.NewGuid(),
            ParticipantId = Guid.NewGuid(),
            ContactEmail = $"{Guid.NewGuid()}@intautomation.com",
            Username = $"{Guid.NewGuid()}@intautomation.com",
            RandomPassword = "12345678dusausyd",
            ScheduledDateTime = DateTime.UtcNow.AddDays(1),
            TotalDays = 1
        };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantMultiDayHearingConfirmationForNewUserEmail, RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var validationProblemDetails = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
        validationProblemDetails.Errors["request"].ToList()
            .Exists(errorMessage => errorMessage.Contains("Role is not supported")).Should().BeTrue();
            
        _notifyStub.SentEmails.Count.Should().Be(0);
    }
        
    [Test]
    public async Task should_send_a_multi_day_confirmation_email_for_a_new_lip()
    {
         // arrange
         var request = new NewUserMultiDayHearingConfirmationRequest()
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
             ScheduledDateTime = DateTime.UtcNow.AddDays(1),
             TotalDays = 1
         };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantMultiDayHearingConfirmationForNewUserEmail, RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);
  
        var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
            Domain.Enums.NotificationType.NewUserLipConfirmationMultiDay,
            request.ContactEmail);
        notifications.Count.Should().Be(1);
        _notifyStub.SentEmails.Count.Should().Be(1);
        _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                           && x.ExternalRefId == notifications[0].ExternalId 
        ).Should().BeTrue();
    }
    
    [Test]
    public async Task should_send_a_multi_day_confirmation_email_for_a_new_representative()
    {
        // arrange
        var request = new NewUserMultiDayHearingConfirmationRequest()
        {
            RoleName = RoleNames.Representative,
            Name = $"{Faker.Name.FullName()}",
            CaseNumber = $"{Faker.RandomNumber.Next()}",
            CaseName = $"{Faker.RandomNumber.Next()}",
            HearingId = Guid.NewGuid(),
            ParticipantId = Guid.NewGuid(),
            ContactEmail = $"{Guid.NewGuid()}@intautomation.com",
            Username = $"{Guid.NewGuid()}@intautomation.com",
            RandomPassword = "12345678dusausyd",
            ScheduledDateTime = DateTime.UtcNow.AddDays(1),
            TotalDays = 1
        };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantMultiDayHearingConfirmationForNewUserEmail, RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);
  
        var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
            Domain.Enums.NotificationType.NewUserRepresentativeConfirmationMultiDay,
            request.ContactEmail);
        notifications.Count.Should().Be(1);
        _notifyStub.SentEmails.Count.Should().Be(1);
        _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                           && x.ExternalRefId == notifications[0].ExternalId 
        ).Should().BeTrue();
    }
}
