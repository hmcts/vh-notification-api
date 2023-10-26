using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;
using NotificationApi.IntegrationTests.Api.Setup;
using NotificationApi.IntegrationTests.Helper;
using NotificationApi.IntegrationTests.Stubs;
using NotificationApi.Validations;
using Notify.Interfaces;
using NUnit.Framework;
using Testing.Common.Helper;

namespace NotificationApi.IntegrationTests.Api
{
    public class SendParticipantWelcomeEmailTests : ApiTest
    {
        private AsyncNotificationClientStub _notifyStub;

        [SetUp]
        public void Setup()
        {
            var scope = Application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _notifyStub = scope.ServiceProvider.GetRequiredService<IAsyncNotificationClient>() as AsyncNotificationClientStub;
            _notifyStub!.SentEmails.Clear();
        }
        
        [Test]
        public async Task should_return_validation_errors()
        {
            // arrange
            var request = new NewUserWelcomeEmailRequest
            {
                RoleName = RoleNames.Representative,
            };

            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantWelcomeEmail, RequestBody.Set(request));

            // assert
            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var validationProblemDetails = await ApiClientResponse.GetResponses<ValidationProblemDetails>(result.Content);
            validationProblemDetails.Errors[$"{nameof(request.CaseName)}"].ToList()
                .Exists(errorMessage => errorMessage.Contains("must not be empty")).Should().BeTrue();
            validationProblemDetails.Errors[$"{nameof(request.RoleName)}"].Should().Contain(NewUserWelcomeEmailRequestValidation.UnsupportedRoleMessage);
            
            _notifyStub.SentEmails.Count.Should().Be(0);
        }

        [Test]
        public async Task should_return_ok()
        {
            // arrange
            var request = new NewUserWelcomeEmailRequest
            {
                Name = $"{Faker.Name.FullName()}",
                CaseNumber = $"{Faker.RandomNumber.Next()}",
                CaseName = $"{Faker.RandomNumber.Next()}",
                RoleName = RoleNames.Individual,
                HearingId = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                ContactEmail = $"{Guid.NewGuid()}@intautomation.com"
            };
            
            // act
            using var client = Application.CreateClient();
            var result = await client.PostAsync(
                ApiUriFactory.ParticipantNotificationEndpoints.SendParticipantWelcomeEmail, RequestBody.Set(request));

            
            // assert
            result.IsSuccessStatusCode.Should().BeTrue(result.Content.ReadAsStringAsync().Result);

            var notifications = await TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
                (Domain.Enums.NotificationType) NotificationType.NewUserLipWelcome,
                request.ContactEmail);
            
            notifications.Count.Should().Be(1);
            _notifyStub.SentEmails.Count.Should().Be(1);
            _notifyStub.SentEmails.Exists(x => x.EmailAddress == request.ContactEmail 
                                               && x.ExternalRefId == notifications[0].ExternalId 
            ).Should().BeTrue();
            
        }
    }
}
