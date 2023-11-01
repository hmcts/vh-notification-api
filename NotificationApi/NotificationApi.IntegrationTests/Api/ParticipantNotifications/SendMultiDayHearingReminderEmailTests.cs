namespace NotificationApi.IntegrationTests.Api.ParticipantNotifications;

public class SendMultiDayHearingReminderEmailTests : ApiTest
{
    [TestCase(RoleNames.Representative)]
    [TestCase(RoleNames.Judge)]
    [TestCase(RoleNames.PanelMember)]
    [TestCase(RoleNames.JudicialOfficeHolder)]
    [TestCase(RoleNames.Winger)]
    public async Task should_not_send_a_confirmation_email_for_a(string roleName)
    {
        throw new NotImplementedException();
    }
        
    [Test]
    public async Task should_send_a_confirmation_email_for_a_lip()
    {
        throw new NotImplementedException();
    }
}
