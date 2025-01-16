using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            
            return apiClient;
        }
        
        public static NotificationApiClient GetClient(string baseUrl, HttpClient httpClient)
        {
            var apiClient = GetClient(httpClient);
            apiClient.BaseUrl = baseUrl;
            return apiClient;
        }
        
        static partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
        {
            settings.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            settings.WriteIndented = true;
            settings.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
