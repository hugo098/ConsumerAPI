using UtilityLibrary;

namespace JSONPlaceholderConsumer.Services.IServices
{
    public interface IBaseService
    { 
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
