using AutoMapper;
using ConsumerAPI.Controllers.v1;
using ConsumerAPI.Models.Posts;
using ConsumerAPI.Models.Posts.Dtos;
using ConsumerAPI.Models.Users;
using ConsumerAPI.Services.IServices;
using Moq;
using System.Net;
using UtilityLibrary;

namespace ConsumerAPITestV1
{
    public class PostControllerTest
    {
        [Fact]
        public void GetAllPosts_ShouldReturnAllPosts()
        {
            //ARRANGE
            var mockedService = new Mock<IJSONPlaceholderService>();
            var mockedMapper = new Mock<IMapper>();

            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    UserId = 1,
                    Id = 1,
                    Title = "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
                    Body = "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
                },
                new Post()
                {
                    UserId = 1,
                    Id = 2,
                    Title = "qui est esse",
                    Body = "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
                }
            };
            List<PostDTO> postsDto = posts.Select(x =>
            {
                return new PostDTO { Id = x.Id, Body = x.Body, Title = x.Title, UserId = x.UserId };
            }).ToList();

            mockedService.Setup(x => x.GetAllAsync<List<Post>>()).Returns(Task.FromResult(posts));

            mockedMapper.Setup(x => x.Map<List<PostDTO>>(posts)).Returns(postsDto);

            var controller = new PostController(mockedService.Object,
                mockedService.Object,
                mockedMapper.Object);


            //ACT
            var result = controller.GetPosts().Result as object;
            var actualResult = result.GetType().GetProperty("Result").GetValue(result, null);
            var response = actualResult.GetType().GetProperty("Value").GetValue(actualResult, null) as APIResponse;

            //ASSERT
            Assert.IsType<APIResponse>(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.IsType<List<PostDTO>>(response.Result);
            Assert.True(response.IsSuccess);

        }

        [Fact]
        public void GetPost_ShouldReturnOnePost()
        {
            int postId = 1;
            //ARRANGE
            var mockedService = new Mock<IJSONPlaceholderService>();
            var mockedMapper = new Mock<IMapper>();

            Post post = new Post()
            {
                UserId = 1,
                Id = 2,
                Title = "qui est esse",
                Body = "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
            };

            PostDTO postDto = new PostDTO()
            {
                UserId = post.UserId,
                Id = post.Id,
                Title = post.Title,
                Body = post.Body
            };

            mockedService.Setup(x => x.GetAsync<Post>(postId)).Returns(Task.FromResult(post));

            mockedMapper.Setup(x => x.Map<PostDTO>(post)).Returns(postDto);

            var controller = new PostController(mockedService.Object,
                mockedService.Object,
                mockedMapper.Object);

            //ACT
            var result = controller.GetPost(postId).Result as object;
            var actualResult = result.GetType().GetProperty("Result").GetValue(result, null);
            var response = actualResult.GetType().GetProperty("Value").GetValue(actualResult, null) as APIResponse;

            //ASSERT
            Assert.IsType<APIResponse>(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.IsType<PostDTO>(response.Result);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public void CreatePost_ShouldCreateOnePost()
        {
            //ARRANGE
            var mockedService = new Mock<IJSONPlaceholderService>();
            var mockedMapper = new Mock<IMapper>();

            CreatePostDTO createPostDto = new CreatePostDTO()
            {
                UserId = 1,
                Title = "testing",
                Body = "this is a test",
            };

            Post post = new Post()
            {
                UserId = 1,
                Id = 101,
                Title = "testing",
                Body = "this is a test"
            };

            PostDTO postDTO = new PostDTO()
            {
                Body = post.Body,
                UserId = post.UserId,
                Title = post.Title,
                Id = post.Id,
            };

            User user = new User()
            {
                Id = 1,
                Email = "testing",
                Name = "testing",
                Phone = "testing",
                Username = "testing",
                Website = "testing",
            };

            mockedService.Setup(x => x.GetAsync<User>(createPostDto.UserId)).Returns(Task.FromResult(user));

            mockedService.Setup(x => x.CreateAsync<Post>(createPostDto)).Returns(Task.FromResult(post));

            mockedMapper.Setup(x => x.Map<PostDTO>(post)).Returns(postDTO);

            var controller = new PostController(mockedService.Object,
                mockedService.Object,
                mockedMapper.Object);

            //ACT
            var result = controller.CreatePost(createPostDto).Result as object;
            var actualResult = result.GetType().GetProperty("Result").GetValue(result, null);
            var response = actualResult.GetType().GetProperty("Value").GetValue(actualResult, null) as APIResponse;

            //ASSERT
            Assert.IsType<APIResponse>(response);
            Assert.IsType<User>(user);
            Assert.NotNull(user);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.IsType<PostDTO>(response.Result);
            Assert.True(response.IsSuccess);
            Assert.Equal(createPostDto.Body, post.Body);
        }

        [Fact]
        public void UpdatePost_ShouldUpdateOnePost()
        {
            //ARRANGE
            var mockedService = new Mock<IJSONPlaceholderService>();
            var mockedMapper = new Mock<IMapper>();

            int postId = 1;

            UpdatePostDTO updatePostDto = new UpdatePostDTO()
            {
                UserId = 1,
                Title = "testing updated",
                Body = "this is a test updated",
            };

            Post post = new Post()
            {
                UserId = 1,
                Id = postId,
                Title = "testing",
                Body = "this is a test"
            };

            Post updatedPost = new Post()
            {
                Body = updatePostDto.Body,
                UserId = updatePostDto.UserId,
                Title = updatePostDto.Title,
                Id = post.Id,
            };

            PostDTO postDTO = new PostDTO()
            {
                Body = updatedPost.Body,
                UserId = updatedPost.UserId,
                Title = updatedPost.Title,
                Id = updatedPost.Id,
            };

            User user = new User()
            {
                Id = 1,
                Email = "testing",
                Name = "testing",
                Phone = "testing",
                Username = "testing",
                Website = "testing",
            };

            mockedService.Setup(x => x.GetAsync<User>(updatePostDto.UserId)).Returns(Task.FromResult(user));

            mockedService.Setup(x => x.GetAsync<Post>(postId)).Returns(Task.FromResult(post));

            mockedService.Setup(x => x.UpdateAsync<Post>(postId, updatePostDto)).Returns(Task.FromResult(updatedPost));

            mockedMapper.Setup(x => x.Map<PostDTO>(updatedPost)).Returns(postDTO);

            var controller = new PostController(mockedService.Object,
                mockedService.Object,
                mockedMapper.Object);

            //ACT
            var result = controller.UpdatePost(postId, updatePostDto).Result as object;
            var actualResult = result.GetType().GetProperty("Result").GetValue(result, null);
            var response = actualResult.GetType().GetProperty("Value").GetValue(actualResult, null) as APIResponse;

            //ASSERT
            Assert.IsType<APIResponse>(response);
            Assert.IsType<User>(user);
            Assert.NotNull(user);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.IsType<PostDTO>(response.Result);
            Assert.True(response.IsSuccess);
            Assert.Equal(updatePostDto.Title, updatedPost.Title);
            Assert.Equal(updatePostDto.Body, updatedPost.Body);

        }

        [Fact]
        public void DeletePost_ShouldDeleteOnePost()
        {
            //ARRANGE
            var mockedService = new Mock<IJSONPlaceholderService>();
            var mockedMapper = new Mock<IMapper>();

            int postId = 1;

            Post post = new Post()
            {
                UserId = 1,
                Id = postId,
                Title = "testing",
                Body = "this is a test"
            };

            PostDTO postDTO = new PostDTO()
            {
                Body = post.Body,
                UserId = post.UserId,
                Title = post.Title,
                Id = post.Id,
            };

            mockedService.Setup(x => x.GetAsync<Post>(postId)).Returns(Task.FromResult(post));
            mockedService.Setup(x => x.DeleteAsync<Post>(postId)).Returns(Task.FromResult(true));
            mockedMapper.Setup(x => x.Map<PostDTO>(post)).Returns(postDTO);

            var controller = new PostController(mockedService.Object,
                mockedService.Object,
                mockedMapper.Object);

            //ACT
            var result = controller.DeletePost(postId).Result as object;
            var actualResult = result.GetType().GetProperty("Result").GetValue(result, null);
            var response = actualResult.GetType().GetProperty("Value").GetValue(actualResult, null) as APIResponse;

            //ASSERT
            Assert.IsType<APIResponse>(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.IsType<PostDTO>(response.Result);
            Assert.True(response.IsSuccess);
        }
    }
}