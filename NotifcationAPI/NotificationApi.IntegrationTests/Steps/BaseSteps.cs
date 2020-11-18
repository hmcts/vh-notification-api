using System.Net.Http;
using System.Threading.Tasks;
using NotificationApi.IntegrationTests.Contexts;

namespace NotificationApi.IntegrationTests.Steps
{
    public abstract class BaseSteps
    {
        protected async Task<HttpResponseMessage> SendGetRequestAsync(IntTestContext intTestContext)
        {
            using var client = intTestContext.CreateClient();
            return await client.GetAsync(intTestContext.Uri);
        }

        protected async Task<HttpResponseMessage> SendPatchRequestAsync(IntTestContext intTestContext)
        {
            using var client = intTestContext.CreateClient();
            return await client.PatchAsync(intTestContext.Uri, intTestContext.HttpContent);
        }

        protected async Task<HttpResponseMessage> SendPostRequestAsync(IntTestContext intTestContext)
        {
            using var client = intTestContext.CreateClient();
            return await client.PostAsync(intTestContext.Uri, intTestContext.HttpContent);
        }

        protected async Task<HttpResponseMessage> SendPutRequestAsync(IntTestContext intTestContext)
        {
            using var client = intTestContext.CreateClient();
            return await client.PutAsync(intTestContext.Uri, intTestContext.HttpContent);
        }

        protected async Task<HttpResponseMessage> SendDeleteRequestAsync(IntTestContext intTestContext)
        {
            using var client = intTestContext.CreateClient();
            return await client.DeleteAsync(intTestContext.Uri);
        }
    }
}
