using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Contract.Requests;
using NotificationApi.Domain;
using NotificationApi.IntegrationTests.Api.Setup;
using NotificationApi.IntegrationTests.Helper;
using NotificationApi.Validations;
using NUnit.Framework;
using Testing.Common.Helper;

namespace NotificationApi.IntegrationTests.Api
{
    public class CallbackTests : ApiTest
    {
        [Test]
        public async Task should_update_the_delivery_status_a_notification()
        {
            // arrange
            var deliveryStatus = "delivered";
            var notification = await TestDataManager.SeedSendingNotification();
            var request = BuildRequest(notification, deliveryStatus);

            // act
            using var client = Application.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateCallbackToken());
            var result = await client.PostAsync(
                ApiUriFactory.NotificationEndpoints.UpdateNotification, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue();
        }

        [Test]
        public async Task should_return_bad_request_when_the_external_id_does_not_match()
        {
            // arrange
            var deliveryStatus = "delivered";
            var notification = await TestDataManager.SeedSendingNotification();
            var request = BuildRequest(notification, deliveryStatus);
            request.Id = Guid.NewGuid().ToString();

            // act
            using var client = Application.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateCallbackToken());
            var result = await client.PostAsync(
                ApiUriFactory.NotificationEndpoints.UpdateNotification, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var validationProblemDetails = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
            validationProblemDetails.Errors["database"][0].Should()
                .Be($"ExternalId {request.Id} does not belong to Notification {notification.Id}");
        }

        [Test]
        public async Task should_return_bad_request_when_invalid_payload_is_sent()
        {
            // arrange
            var deliveryStatus = "delivered";
            var notification = await TestDataManager.SeedSendingNotification();
            var request = BuildRequest(notification, deliveryStatus);
            request.Id = string.Empty;

            // act
            using var client = Application.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateCallbackToken());
            var result = await client.PostAsync(
                ApiUriFactory.NotificationEndpoints.UpdateNotification, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var validationProblemDetails = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
            validationProblemDetails.Errors[nameof(request.Id)][0].Should()
                .Be(NotificationCallbackRequestValidation.MissingIdMessage);
        }
    
        [TearDown]
        public async Task TearDown()
        {
            await TestDataManager.RemoveNotifications(TestDataManager.NotificationsCreated.Select(x => x.Id));
            TestDataManager.NotificationsCreated.Clear();
        }
    
        private static NotificationCallbackRequest BuildRequest(Notification notification, string deliveryStatus)
        {
            return new NotificationCallbackRequest
            {
                Id = notification.ExternalId,
                Reference = notification.Id.ToString(),
                Status = deliveryStatus
            };
        }
    }
}
