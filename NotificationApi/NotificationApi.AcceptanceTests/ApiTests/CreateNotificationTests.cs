using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;
using NUnit.Framework;
using Testing.Common.Extensions;

namespace NotificationApi.AcceptanceTests.ApiTests
{
    public class CreateNotificationTests : AcApiTest
    {
        [Test]
        public async Task should_return_okay_when_creating_a_create_individual_email_notification_with_a_valid_payload()
        {
            // arrange
            var parameters = new Dictionary<string, string>
            {
                {"name", $"{Faker.Name.FullName()}"},
                {"username", $"{Guid.NewGuid().ToString()}@automation.com"},
                {"random password", "testpassword!"}
            };

            var request =
                AddNotificationRequestBuilder.BuildRequest(MessageType.Email, NotificationType.CreateIndividual,
                    parameters);

            // act
            await NotificationApiClient.CreateNewNotificationAsync(request);

            // assert
            await AssertRequestParamsAreInNotifyResult(request);
        }

        [Test]
        public async Task should_return_okay_when_creating_a_reset_password_email_notification_with_a_valid_payload()
        {
            // arrange
            var parameters = new Dictionary<string, string>
            {
                {"name", Faker.Name.FullName()},
                {"password", "ACTestPasswordReset!"}
            };
            var request =
                AddNotificationRequestBuilder.BuildNonHearingRequest(MessageType.Email, NotificationType.PasswordReset,
                    parameters);

            // act
            await NotificationApiClient.CreateNewNotificationAsync(request);

            // assert
            await AssertRequestParamsAreInNotifyResult(request);
        }

        private async Task AssertRequestParamsAreInNotifyResult(AddNotificationRequest request)
        {
            await AssertNotifyHasMyRequest(notification =>
            {
                foreach (var parameter in request.Parameters)
                {
                    return notification.body.Contains(parameter.Value);
                }

                return false;
            });
        }

        private async Task AssertNotifyHasMyRequest(Func<Notify.Models.Notification, bool> predicate)
        {
            var allNotifications = await NotifyClient.GetNotificationsAsync("email");
            var recentNotification = allNotifications.notifications.LastOrDefault(predicate);
            recentNotification.Should().NotBeNull();
        }
    }
}
