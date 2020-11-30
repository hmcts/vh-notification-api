using FluentAssertions;
using Notify.Models.Responses;

namespace NotificationApi.IntegrationTests.Assertions
{
    public class AssertNotificationResponse
    {
        public static void ForNotification(NotificationResponse notification)
        {
            notification.Should().NotBeNull();
            notification.id.Should().NotBeNullOrEmpty();
            notification.reference.Should().NotBeNullOrEmpty();
            notification.uri.Should().NotBeNullOrEmpty();
            notification.template.id.Should().NotBeNullOrEmpty();
            notification.template.uri.Should().NotBeNullOrEmpty();
            notification.template.version.Should().NotBe(0);
        }
    }
}
