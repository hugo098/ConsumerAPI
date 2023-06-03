using AutoMapper;
using JSONPlaceholderConsumer.Models.Posts;
using JSONPlaceholderConsumer.Models.Posts.Dtos;

namespace JSONPlaceholderConsumer
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();
        }
    }
}
