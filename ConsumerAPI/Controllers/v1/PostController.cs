using AutoMapper;
using JSONPlaceholderConsumer.Models.Posts;
using JSONPlaceholderConsumer.Models.Posts.Dtos;
using JSONPlaceholderConsumer.Models.Users;
using JSONPlaceholderConsumer.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UtilityLibrary;

namespace JSONPlaceholderConsumer.Controllers.v1
{
    [Route("api/v{version:apiVersion}/posts")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PostController : ControllerBase
    {
        private readonly IJSONPlaceholderService _JSONPlaceholderPostService;
        private readonly IJSONPlaceholderService _JSONPlaceholderUserService;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        public PostController(IJSONPlaceholderService JSONPlaceholderPostService,
            IJSONPlaceholderService JSONPlaceholderUserService,
            IMapper mapper)
        {
            _JSONPlaceholderPostService = JSONPlaceholderPostService;
            _JSONPlaceholderUserService = JSONPlaceholderUserService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetPosts()
        {
            try
            {
                List<Post> postList = await _JSONPlaceholderPostService.GetAllAsync<List<Post>>();


                _response.BuildResponse(result: _mapper.Map<List<PostDTO>>(postList), isSuccess: true, statusCode: HttpStatusCode.OK);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.BuildResponse(errors: new List<string>() { ex.ToString() }, isSuccess: false, statusCode: HttpStatusCode.InternalServerError);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }

        }

        [HttpGet("{id:int}", Name = "GetPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetPost(int id)
        {
            try
            {
                Post post = await _JSONPlaceholderPostService.GetAsync<Post>(id);

                if (post == null)
                {
                    _response.BuildResponse(errors: new List<string>() { $"Post with id {id} not found" },
                        isSuccess: false,
                        statusCode: HttpStatusCode.NotFound);
                    return NotFound(_response);
                }

                _response.BuildResponse(result: _mapper.Map<PostDTO>(post), isSuccess: true, statusCode: HttpStatusCode.OK);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.BuildResponse(errors: new List<string>() { ex.ToString() }, isSuccess: false, statusCode: HttpStatusCode.InternalServerError);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreatePost([FromBody] CreatePostDTO createPostDto)
        {
            try
            {
                User user = await _JSONPlaceholderUserService.GetAsync<User>(createPostDto.UserId);

                if(user == null)
                {
                    _response.BuildResponse(errors: new List<string>() { $"User with id {createPostDto.UserId} not found" },
                        isSuccess: false,
                        statusCode: HttpStatusCode.BadRequest);
                    return BadRequest(_response);
                }

                Post post = await _JSONPlaceholderPostService.CreateAsync<Post>(createPostDto);

                _response.BuildResponse(result: _mapper.Map<PostDTO>(post), isSuccess: true, statusCode: HttpStatusCode.Created);

                return CreatedAtRoute("GetPost", new { id = post.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.BuildResponse(errors: new List<string>() { ex.ToString() }, isSuccess: false, statusCode: HttpStatusCode.InternalServerError);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdatePost(int id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            try
            {
                User user = await _JSONPlaceholderUserService.GetAsync<User>(updatePostDTO.UserId);

                if (user == null)
                {
                    _response.BuildResponse(errors: new List<string>() { $"User with id {updatePostDTO.UserId} not found" },
                        isSuccess: false,
                        statusCode: HttpStatusCode.BadRequest);
                    return BadRequest(_response);
                }

                Post post = await _JSONPlaceholderPostService.GetAsync<Post>(id);

                if (post == null)
                {
                    _response.BuildResponse(errors: new List<string>() { $"Post with id {id} not found" },
                        isSuccess: false,
                        statusCode: HttpStatusCode.NotFound);
                    return NotFound(_response);
                }

                Post updatedPost = await _JSONPlaceholderPostService.UpdateAsync<Post>(id, updatePostDTO);                

                _response.BuildResponse(result: _mapper.Map<PostDTO>(updatedPost), isSuccess: true, statusCode: HttpStatusCode.OK);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.BuildResponse(errors: new List<string> { ex.ToString() }, isSuccess: false, statusCode: HttpStatusCode.InternalServerError);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeletePost(int id)
        {
            try
            {
                Post post = await _JSONPlaceholderPostService.GetAsync<Post>(id);

                if (post == null)
                {
                    _response.BuildResponse(errors: new List<string>() { $"Post with id {id} not found" },
                        isSuccess: false,
                        statusCode: HttpStatusCode.NotFound);
                    return NotFound(_response);
                }


                await _JSONPlaceholderPostService.DeleteAsync<Post>(id);

                _response.BuildResponse(result: _mapper.Map<PostDTO>(post), isSuccess: true, statusCode: HttpStatusCode.OK);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.BuildResponse(errors: new List<string> { ex.ToString() }, isSuccess: false, statusCode: HttpStatusCode.InternalServerError);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }

        }


    }
}
