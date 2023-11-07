using System.Collections.Generic;
using NotificationApi.Contract;
using Testing.Common.Extensions;

namespace NotificationApi.IntegrationTests.Api
{
    public class CreateHearingNotificationTests : ApiTest
    {
        [Test]
        public async Task should_create_notification()
        {
            // arrange
            var request = BuildNewUserNotificationRequest(MessageType.Email, NotificationType.CreateIndividual);

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.NotificationEndpoints.CreateNewEmailNotification, RequestBody.Set(request));


            // assert
            result.IsSuccessStatusCode.Should().BeTrue();

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
                (Domain.Enums.NotificationType) request.NotificationType,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
        }

        [Test]
        public async Task should_create_notification_once_when_duplicate_request_is_sent()
        {
            // arrange
            var request = BuildNewUserNotificationRequest(MessageType.Email, NotificationType.CreateIndividual);

            // act
            using var client = Application.CreateClient();
            var result1 = await client.PostAsync(
                ApiUriFactory.NotificationEndpoints.CreateNewEmailNotification, RequestBody.Set(request));
            var result2 = await client.PostAsync(
                ApiUriFactory.NotificationEndpoints.CreateNewEmailNotification, RequestBody.Set(request));

            // assert
            result1.IsSuccessStatusCode.Should().BeTrue();
            result2.IsSuccessStatusCode.Should().BeTrue();

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
                (Domain.Enums.NotificationType) request.NotificationType,
                request.ContactEmail);
            notifications.Count.Should().Be(1);
        }

        [Test]
        public async Task should_return_bad_request_when_an_invalid_payload_is_sent()
        {
            // arrange
            var request = BuildNewUserNotificationRequest(MessageType.Email, NotificationType.CreateIndividual);
            request.ContactEmail = null;

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.NotificationEndpoints.CreateNewEmailNotification, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var validationProblemDetails = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
            validationProblemDetails.Errors[nameof(request.ContactEmail)][0].Should()
                .Be(AddNotificationRequestValidation.MissingEmailMessage);
        }

        private static AddNotificationRequest BuildNewUserNotificationRequest(MessageType messageType,
            NotificationType notificationType)
        {
            var parameters = new Dictionary<string, string>
            {
                {"name", $"{Faker.Name.FullName()}"},
                {"username", $"{Guid.NewGuid()}@intautomation.com"},
                {"random password", "inttestpassword!"}
            };

            return AddNotificationRequestBuilder.BuildRequest(messageType, notificationType, parameters);
        }
    }
}
