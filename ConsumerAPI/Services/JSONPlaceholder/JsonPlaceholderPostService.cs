using ConsumerAPI.Models.Posts.Dtos;
using ConsumerAPI.Services.IServices;
using UtilityLibrary;

namespace ConsumerAPI.Services.JSONPlaceholder
{
    public class JsonPlaceholderPostService : BaseService, IJSONPlaceholderService
    {
        private string JSONPlaceholderURL;

        public JsonPlaceholderPostService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {            
            JSONPlaceholderURL = configuration.GetValue<string>("ServiceUrls:JSONPlaceholderAPI");
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiMethodType = APIMethodType.GET,
                Url = JSONPlaceholderURL + "/posts"
            });
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiMethodType = APIMethodType.GET,
                Url = JSONPlaceholderURL + "/posts/" + id
            });
        }

        public async Task<T> CreateAsync<T>(CreatePostDTO dto)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiMethodType = APIMethodType.POST,
                Data = dto,
                Url = JSONPlaceholderURL + "/posts"
            });
        }

        public async Task DeleteAsync<T>(int id)
        {
            await SendAsync<T>(new APIRequest()
            {
                ApiMethodType = APIMethodType.DELETE,
                Url = JSONPlaceholderURL + "/posts/" + id
            });
        }

        public async Task<T> UpdateAsync<T>(int id, UpdatePostDTO dto)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiMethodType = APIMethodType.PUT,
                Data = dto,
                Url = JSONPlaceholderURL + "/posts/" + id,
            });
        }
    }
}
