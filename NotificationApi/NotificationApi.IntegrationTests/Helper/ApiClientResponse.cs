using System.Net.Http;

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
