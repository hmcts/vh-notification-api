using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.Domain.Enums;
using NotificationApi.IntegrationTests.Assertions;
using NotificationApi.IntegrationTests.Contexts;
using NotificationApi.IntegrationTests.Helper;
using Notify.Models.Responses;
using TechTalk.SpecFlow;
using Testing.Common.Helper;

namespace NotificationApi.IntegrationTests.Steps
{
    [Binding]
    public class NotificationsSteps
    {
        private readonly IntTestContext _context;
        private NotificationResponse _notification;
        
        public NotificationsSteps(IntTestContext context)
        {
            _context = context;
        }
        
        [Given("I have a valid create new email notification request")]
        public void I_have_a_valid_create_new_email_notification_request()
        {
            _context.Uri = ApiUriFactory.NotificationEndpoints.CreateNewEmailNotificationResponse();
            _context.HttpMethod = HttpMethod.Post;
        }
        
        [Then("the response should have the status (.*)")]
        public void the_response_should_have_the_status_created(HttpStatusCode statusCode)
        {
            _context.Response.Should().Be(statusCode);
        }
        
        [Then("the success status should be (.*)")]
        public void the_success_status_should_be_true(bool isSuccess)
        {
            _context.Response.IsSuccessStatusCode.Should().Be(isSuccess);
        }
        
        [Then("the notification details should be retrieved")]
        public async Task the_notification_details_should_be_retrieved()
        {
            _notification = await Response.GetResponses<NotificationResponse>(_context.Response.Content);
            _notification.Should().NotBeNull();
            AssertNotificationResponse.ForNotification(_notification);   
        }
    }
}
