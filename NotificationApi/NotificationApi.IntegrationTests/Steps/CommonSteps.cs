using System;
using System.Threading.Tasks;
using NotificationApi.IntegrationTests.Contexts;
using TechTalk.SpecFlow;

namespace NotificationApi.IntegrationTests.Steps
{
    public class CommonSteps : BaseSteps
    {
        private readonly IntTestContext _context;

        public CommonSteps(IntTestContext context)
        {
            _context = context;
        }
        
        [When("I send the request to the endpoint")]
        public async Task I_handle_the_request()
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
    }
}
