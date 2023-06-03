using UtilityLibrary;

namespace ConsumerAPI.Services.IServices
{
    public interface IBaseService
    { 
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
