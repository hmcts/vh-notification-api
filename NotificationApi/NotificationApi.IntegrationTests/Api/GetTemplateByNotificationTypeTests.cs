using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Contract.Responses;
using NotificationApi.DAL;
using NotificationApi.IntegrationTests.Api.Setup;
using NotificationApi.IntegrationTests.Helper;
using NUnit.Framework;
using Testing.Common.Helper;

namespace NotificationApi.IntegrationTests.Api
{
    public class GetTemplateByNotificationTypeTests : ApiTest
    {
        [Test]
        public async Task should_get_template_details_for_a_notification_type()
        {
            // arrange
            var notificationType = Contract.NotificationType.CreateIndividual;
        
            // act
            using var client = Application.CreateClient();
            var result = await client.GetAsync(
                ApiUriFactory.NotificationEndpoints.GetTemplateByNotificationType((int) notificationType));

            // assert
            result.IsSuccessStatusCode.Should().BeTrue();
            var response = await ApiClientResponse.GetResponses<NotificationTemplateResponse>(result.Content);
            response.Should().NotBeNull();
            response.NotificationType.Should().BeEquivalentTo(notificationType);
        }

        [Test]
        public async Task should_return_not_found_when_a_non_existent_template_type_is_requested()
        {
            // arrange
            var notificationType = 9999;
        
            // act
            using var client = Application.CreateClient();
            var result = await client.GetAsync(
                ApiUriFactory.NotificationEndpoints.GetTemplateByNotificationType(notificationType));

            // assert
            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseBody = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
            var errors = responseBody.Errors["notificationType"];
            errors[0].Should().Be($"The value '{notificationType}' is invalid.");
        }

        [Test]
        public async Task should_return_bad_request_when_template_does_not_exist()
        {
            // arrange
            using var client = Application.CreateClient(); // need to call first to startup api before clearing db
            await using var db = new NotificationsApiDbContext(DbOptions);
            db.Templates.RemoveRange(db.Templates);
            await db.SaveChangesAsync();
        
            var notificationType = Contract.NotificationType.CreateIndividual;
        
            // act
            var result = await client.GetAsync(
                ApiUriFactory.NotificationEndpoints.GetTemplateByNotificationType((int) notificationType));

            // assert
            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseBody = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
            var errors = responseBody.Errors["request"];
            errors[0].Should().Be($"Invalid notificationType: CreateIndividual");
        }
    }
}
