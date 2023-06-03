using UtilityLibrary;

namespace JSONPlaceholderConsumer.Services.JSONPlaceholder
{
    public class JsonPlaceholderUserService : BaseService
    {
        private string JSONPlaceholderURL;

        public JsonPlaceholderUserService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {            
            JSONPlaceholderURL = configuration.GetValue<string>("ServiceUrls:JSONPlaceholderAPI");
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiMethodType = APIMethodType.GET,
                Url = JSONPlaceholderURL + "/users/" + id
            });
        }
    }
}
