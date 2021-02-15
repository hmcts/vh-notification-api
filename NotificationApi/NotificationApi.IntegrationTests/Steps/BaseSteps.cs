using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AcceptanceTests.Common.Api.Helpers;
using NotificationApi.Contract.Requests;
using NotificationApi.IntegrationTests.Contexts;
using Testing.Common.Helper;

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
        
        protected void InitCreateNotificationRequest(AddNotificationRequest request, IntTestContext intTestContext)
        {
            intTestContext.Uri = ApiUriFactory.NotificationEndpoints.CreateNewEmailNotification;
            intTestContext.HttpMethod = HttpMethod.Post;
            var body = RequestHelper.Serialise(request);
            intTestContext.HttpContent = new StringContent(body, Encoding.UTF8, "application/json");
        }
    }
}
