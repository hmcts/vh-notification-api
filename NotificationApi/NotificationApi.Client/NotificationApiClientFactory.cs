using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NotificationApi.Client
{
    public partial class NotificationApiClient
    {
        public static NotificationApiClient GetClient(HttpClient httpClient)
        {
            var apiClient = new NotificationApiClient(httpClient)
            {
                ReadResponseAsString = true
            };
            apiClient.JsonSerializerSettings.ContractResolver = new DefaultContractResolver {NamingStrategy = new SnakeCaseNamingStrategy()};
            apiClient.JsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            apiClient.JsonSerializerSettings.Converters.Add(new StringEnumConverter());
            return apiClient;
        }
    }
}
