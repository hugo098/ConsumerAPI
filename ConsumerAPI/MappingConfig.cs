using AutoMapper;
using ConsumerAPI.Models.Posts;
using ConsumerAPI.Models.Posts.Dtos;
using ConsumerAPI.Models.Products.Dtos;
using ConsumerAPI.Models.Products;

namespace ConsumerAPI
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
        }
    }
}
