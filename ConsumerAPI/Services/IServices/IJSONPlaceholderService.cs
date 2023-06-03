using ConsumerAPI.Models.Posts.Dtos;

namespace ConsumerAPI.Services.IServices
{
    public interface IJSONPlaceholderService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(CreatePostDTO dto);
        Task<T> UpdateAsync<T>(int id, UpdatePostDTO dto);
        Task DeleteAsync<T>(int id);

    }
}
