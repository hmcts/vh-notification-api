namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications;

public class SendHearingAmendmentEmailTests : ApiTest
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
    public async Task should_send_a_hearing_amendment_email_for_a_non_judiciary_judge()
    {
        // arrange
        var request = new HearingAmendmentRequest
        {
            DisplayName = "Judge Fudge",
            RoleName = RoleNames.Judge,
            Name = $"{Faker.Name.FullName()}",
            CaseNumber = $"{Faker.Random.Number(1,1000)}",
            CaseName = $"{Faker.Random.Number(1,1000)}",
            HearingId = Guid.NewGuid(),
            ParticipantId = Guid.NewGuid(),
            ContactEmail = $"{Guid.NewGuid()}@test.com",
            Username = $"{Guid.NewGuid()}@test.com",
            PreviousScheduledDateTime = DateTime.UtcNow.AddDays(1),
            NewScheduledDateTime = DateTime.UtcNow.AddDays(2),
        };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendHearingAmendmentEmail,
            RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

        var notifications = await TestDataManager.GetNotifications(request.HearingId,
            request.ParticipantId, Domain.Enums.NotificationType.HearingAmendmentJudge,
            request.ContactEmail);
        notifications.Count.Should().Be(1);
        _notifyStub.SentEmails.Count.Should().Be(1);
        _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail
                                           && x.ExternalRefId == notifications[0].ExternalId
        ).Should().BeTrue();
    }

    [Test]
    public async Task should_send_a_hearing_amendment_email_for_a_judiciary_judge()
    {
        // arrange
        var request = new HearingAmendmentRequest
        {
            DisplayName = "Ejud Judge Fudge",
            RoleName = RoleNames.Judge,
            Name = $"{Faker.Name.FullName()}",
            CaseNumber = $"{Faker.Random.Number(1,1000)}",
            CaseName = $"{Faker.Random.Number(1,1000)}",
            HearingId = Guid.NewGuid(),
            ParticipantId = Guid.NewGuid(),
            ContactEmail = $"{Guid.NewGuid()}@judiciary.com",
            Username = $"{Guid.NewGuid()}@judiciary.com",
            PreviousScheduledDateTime = DateTime.UtcNow.AddDays(1),
            NewScheduledDateTime = DateTime.UtcNow.AddDays(2),
        };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendHearingAmendmentEmail,
            RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

        var notifications = await TestDataManager.GetNotifications(request.HearingId,
            request.ParticipantId, Domain.Enums.NotificationType.HearingAmendmentEJudJudge,
            request.ContactEmail);
        notifications.Count.Should().Be(1);
        _notifyStub.SentEmails.Count.Should().Be(1);
        _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail
                                           && x.ExternalRefId == notifications[0].ExternalId
        ).Should().BeTrue();
    }

    [Test]
    public async Task should_send_a_hearing_amendment_email_for_a_non_judiciary_judicial_office_holder()
    {
        // arrange
        var request = new HearingAmendmentRequest
        {
            RoleName = RoleNames.JudicialOfficeHolder,
            Name = $"{Faker.Name.FullName()}",
            CaseNumber = $"{Faker.Random.Number(1,1000)}",
            CaseName = $"{Faker.Random.Number(1,1000)}",
            HearingId = Guid.NewGuid(),
            ParticipantId = Guid.NewGuid(),
            ContactEmail = $"{Guid.NewGuid()}@test.com",
            Username = $"{Guid.NewGuid()}@test.com",
            PreviousScheduledDateTime = DateTime.UtcNow.AddDays(1),
            NewScheduledDateTime = DateTime.UtcNow.AddDays(2),
        };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendHearingAmendmentEmail, RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

        var notifications = await TestDataManager.GetNotifications(request.HearingId,
            request.ParticipantId, Domain.Enums.NotificationType.HearingAmendmentJoh,
            request.ContactEmail);
        notifications.Count.Should().Be(1);
        _notifyStub.SentEmails.Count.Should().Be(1);
        _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail
                                           && x.ExternalRefId == notifications[0].ExternalId
        ).Should().BeTrue();
    }

    [Test]
    public async Task should_send_a_hearing_amendment_email_for_a_judiciary_judicial_office_holder()
    {
        // arrange
        var request = new HearingAmendmentRequest
        {
            RoleName = RoleNames.JudicialOfficeHolder,
            Name = $"{Faker.Name.FullName()}",
            CaseNumber = $"{Faker.Random.Number(1,1000)}",
            CaseName = $"{Faker.Random.Number(1,1000)}",
            HearingId = Guid.NewGuid(),
            ParticipantId = Guid.NewGuid(),
            ContactEmail = $"{Guid.NewGuid()}@judiciary.com",
            Username = $"{Guid.NewGuid()}@judiciary.com",
            PreviousScheduledDateTime = DateTime.UtcNow.AddDays(1),
            NewScheduledDateTime = DateTime.UtcNow.AddDays(2),
        };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendHearingAmendmentEmail, RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

        var notifications = await TestDataManager.GetNotifications(request.HearingId,
            request.ParticipantId, Domain.Enums.NotificationType.HearingAmendmentEJudJoh,
            request.ContactEmail);
        notifications.Count.Should().Be(1);
        _notifyStub.SentEmails.Count.Should().Be(1);
        _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail
                                           && x.ExternalRefId == notifications[0].ExternalId
        ).Should().BeTrue();
    }
    
    [Test]
    public async Task should_send_a_hearing_amendment_email_for_a_lip()
    {
        // arrange
        var request = new HearingAmendmentRequest
        {
            RoleName = RoleNames.Individual,
            Name = $"{Faker.Name.FullName()}",
            CaseNumber = $"{Faker.Random.Number(1,1000)}",
            CaseName = $"{Faker.Random.Number(1,1000)}",
            HearingId = Guid.NewGuid(),
            ParticipantId = Guid.NewGuid(),
            ContactEmail = $"{Guid.NewGuid()}@test.com",
            Username = $"{Guid.NewGuid()}@test.com",
            PreviousScheduledDateTime = DateTime.UtcNow.AddDays(1),
            NewScheduledDateTime = DateTime.UtcNow.AddDays(2),
        };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendHearingAmendmentEmail,
            RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

        var notifications = await TestDataManager.GetNotifications(request.HearingId,
            request.ParticipantId, Domain.Enums.NotificationType.HearingAmendmentLip,
            request.ContactEmail);
        notifications.Count.Should().Be(1);
        _notifyStub.SentEmails.Count.Should().Be(1);
        _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail
                                           && x.ExternalRefId == notifications[0].ExternalId
        ).Should().BeTrue();
    }
    
    [Test]
    public async Task should_send_a_hearing_amendment_email_for_a_representative()
    {
        // arrange
        var request = new HearingAmendmentRequest
        {
            RoleName = RoleNames.Representative,
            Name = $"{Faker.Name.FullName()}",
            CaseNumber = $"{Faker.Random.Number(1,1000)}",
            CaseName = $"{Faker.Random.Number(1,1000)}",
            HearingId = Guid.NewGuid(),
            ParticipantId = Guid.NewGuid(),
            ContactEmail = $"{Guid.NewGuid()}@test.com",
            Username = $"{Guid.NewGuid()}@test.com",
            PreviousScheduledDateTime = DateTime.UtcNow.AddDays(1),
            NewScheduledDateTime = DateTime.UtcNow.AddDays(2),
            Representee = "John Doe"
        };

        // act
        using var client = Application.CreateClient();
        var result = await client.PostAsync(
            ApiUriFactory.ParticipantNotificationEndpoints.SendHearingAmendmentEmail,
            RequestBody.Set(request));

        // assert
        result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

        var notifications = await TestDataManager.GetNotifications(request.HearingId,
            request.ParticipantId, Domain.Enums.NotificationType.HearingAmendmentRepresentative,
            request.ContactEmail);
        notifications.Count.Should().Be(1);
        _notifyStub.SentEmails.Count.Should().Be(1);
        _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail
                                           && x.ExternalRefId == notifications[0].ExternalId
        ).Should().BeTrue();
    }
}
