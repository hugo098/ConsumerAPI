using ConsumerAPI.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using UtilityLibrary;

namespace ConsumerAPI.Services
{
    public class BaseService : IBaseService
    {
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            var client = httpClient.CreateClient("MyService");
            HttpRequestMessage message = new HttpRequestMessage();

            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(uriString: apiRequest.Url);
            if (apiRequest.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
            }

            message.Method = apiRequest.ApiMethodType switch
            {
                APIMethodType.POST => HttpMethod.Post,
                APIMethodType.PUT => HttpMethod.Put,
                APIMethodType.DELETE => HttpMethod.Delete,
                APIMethodType.GET => HttpMethod.Get,
                _ => HttpMethod.Get
            };           

            HttpResponseMessage apiResponse = await client.SendAsync(message);

            if (!apiResponse.IsSuccessStatusCode)
            {
                return default(T);
            }

            var apiContentSerialized = await apiResponse.Content.ReadAsStringAsync();

            var apiContentDeserialized = JsonConvert.DeserializeObject<T>(apiContentSerialized);

            return apiContentDeserialized;
        }
    }
}

