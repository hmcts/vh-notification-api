using System.Net.Http;
using System.Threading.Tasks;
using Testing.Common.Helper;

namespace NotificationApi.IntegrationTests.Helper
{
    public static class ApiClientResponse
    {
        public static async Task<T> GetResponses<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return RequestHelper.Deserialise<T>(json);
        }
    }
}
