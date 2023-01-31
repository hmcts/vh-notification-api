using System;
using System.Threading.Tasks;
using NotificationApi.IntegrationTests.Contexts;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using NotificationApi.Contract.Requests;
using NotificationApi.Domain.Enums;
using Newtonsoft.Json.Serialization;

namespace NotificationApi.IntegrationTests.Steps
{
    [Binding]
    public class CommonSteps : BaseSteps
    {
        private readonly IntTestContext _context;

        public CommonSteps(IntTestContext context)
        {
            _context = context;
        }
        
        [When(@"I send the request")]
        [Then(@"I send the request")]
        public async Task WhenISendTheRequestToTheEndpoint()
        {
            _context.Response = _context.HttpMethod.Method switch
            {
                "GET" => await SendGetRequestAsync(_context),
                "POST" => await SendPostRequestAsync(_context),
                "PATCH" => await SendPatchRequestAsync(_context),
                "PUT" => await SendPutRequestAsync(_context),
                "DELETE" => await SendDeleteRequestAsync(_context),
                _ => throw new ArgumentOutOfRangeException(_context.HttpMethod.ToString(),
                    _context.HttpMethod.ToString(), null)
            };
        }

        [Then(@"the response should have the status (.*) and success status (.*)")]
        public void ThenTheResponseShouldHaveStatus(HttpStatusCode statusCode, bool isSuccess)
        {
            _context.Response.StatusCode.Should().Be(statusCode);
            _context.Response.IsSuccessStatusCode.Should().Be(isSuccess);
            TestContext.WriteLine($"Status Code: {_context.Response.StatusCode}");
        }
        
        [Given(@"I have a notification")]
        public async Task GivenIHaveANotification()
        {
            var notification = await _context.TestDataManager.SeedCreatedNotification();
            _context.TestRun.NotificationsCreated.Add(notification);
            TestContext.WriteLine($"New seeded notification id: {notification.Id}");
        }
        
        [Given(@"I have a notification that has been sent")]
        public async Task GivenIHaveAConference()
        {
            var notification = await _context.TestDataManager.SeedSendingNotification();
            _context.TestRun.NotificationsCreated.Add(notification);
            TestContext.WriteLine($"New seeded conference id: {notification.Id}");
        }

        [Then(@"there should only be one notification")]
        public async Task ThenThereShouldOnlyBeOneNotification()
        {
            var request = JsonConvert.DeserializeObject<AddNotificationRequest>(_context.HttpContent.ReadAsStringAsync().Result, 
                new JsonSerializerSettings { ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() } });

            var notifications = await _context.TestDataManager.GetNotifications(request.HearingId.Value, request.ParticipantId.Value,
                (NotificationType)request.NotificationType, request.ContactEmail);
            notifications.Count.Should().Be(1);
        }
    }
}
