using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AcceptanceTests.Common.Api;
using AcceptanceTests.Common.Api.Helpers;
using AcceptanceTests.Common.AudioRecordings;
using NotificationApi.Client;
using RestSharp;
using Testing.Common.Configuration;

namespace NotificationApi.AcceptanceTests.Contexts
{
    public class AcTestContext
    {
        public Config Config { get; set; }
        public RestRequest Request { get; set; }
        public IRestResponse Response { get; set; }
        public NotificationApiTokens Tokens { get; set; }
        public AzureStorageManager AzureStorage { get; set; }
        public NotificationApiClient ApiClient { get; set; }
        public object ApiClientResponse { get; set; }
        public string ApiClientMessage { get; set; }

        public RestClient Client()
        {
            var client = new RestClient(Config.ServicesConfig.NotificationApiUrl) {Proxy = Zap.WebProxy};
            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Authorization", $"Bearer {Tokens.NotificationApiBearerToken}");
            return client;
        }

        public async Task ExecuteApiRequest<T>(Func<Task<T>> apiFunc)
        {
            try
            {
                var result = await apiFunc();
                ApiClientResponse = result;
            }
            catch (NotificationApiException e)
            {
                ApiClientResponse = e.Response;
                ApiClientMessage = e.Message;
            }
        }


        public RestRequest Get(string path)
        {
            return new RestRequest(path, Method.GET);
        }

        public RestRequest Post(string path)
        {
            return new RestRequest(path, Method.POST);
        }

        public RestRequest Post(string path, object requestBody)
        {
            var request = new RestRequest(path, Method.POST);            
            request.AddParameter("Application/json", RequestHelper.Serialise(requestBody),
                ParameterType.RequestBody);
            return request;
        }

        public RestRequest Delete(string path)
        {
            return new RestRequest(path, Method.DELETE);
        }

        public RestRequest Put(string path, object requestBody)
        {
            var request = new RestRequest(path, Method.PUT);
            request.AddParameter("Application/json", RequestHelper.Serialise(requestBody),
                ParameterType.RequestBody);
            return request;
        }

        public RestRequest Patch(string path, object requestBody = null)
        {
            var request = new RestRequest(path, Method.PATCH);
            request.AddParameter("Application/json", RequestHelper.Serialise(requestBody),
                ParameterType.RequestBody);
            return request;
        }
    }
}
