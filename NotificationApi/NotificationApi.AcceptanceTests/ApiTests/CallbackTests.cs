using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;
using Notify.Models;
using NUnit.Framework;
using Testing.Common.Extensions;

namespace NotificationApi.AcceptanceTests.ApiTests;

public class CallbackTests : AcApiTest
{
    private Notification _notification;

    [SetUp]
    public async Task Setup()
    {
        _notification = await SeedNotification();
    }

    [Test]
    public void should_return_okay_and_update_delivery_status_for_an_existing_notification()
    {
        var callbackRequest = new NotificationCallbackRequest
        {
            Id = _notification.id,
            Reference = _notification.reference,
            Status = "delivered"
        };

        Assert.DoesNotThrowAsync(async () => await NotificationApiCallbackClient.HandleCallbackAsync(callbackRequest));
    }

    private async Task<Notification> SeedNotification()
    {
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

        var allNotifications = await NotifyClient.GetNotificationsAsync("email");
        var recentNotification = allNotifications.notifications.LastOrDefault(notification =>
        {
            foreach (var parameter in request.Parameters)
            {
                return notification.body.Contains(parameter.Value);
            }

            return false;
        });

        return recentNotification;
    }
}
