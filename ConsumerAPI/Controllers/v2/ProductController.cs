using AutoMapper;
using ConsumerAPI.Models.Products;
using ConsumerAPI.Models.Products.Dtos;
using ConsumerAPI.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using UtilityLibrary;

namespace ConsumerAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/product")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ProductController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IProductRepository _dbProduct;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository dbProduct, IMapper mapper)
        {
            _dbProduct = dbProduct;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetProducts([FromQuery] string? nameFilter,
            int pageSize = 20, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Product> productList;

                if (nameFilter != null)
                {
                    productList = await _dbProduct.GetAllAsync(u => u.ProductName.ToLower().Contains(nameFilter), pageSize: pageSize,
                      pageNumber: pageNumber);

                }
                else
                {
                    productList = await _dbProduct.GetAllAsync(pageSize: pageSize,
                        pageNumber: pageNumber);
                }            

                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                _response.BuildResponse(result: _mapper.Map<List<ProductDTO>>(productList),
                    statusCode: HttpStatusCode.OK,
                    isSuccess: true
                    );
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.BuildResponse(errors: new List<string>() { ex.ToString() },
                   statusCode: HttpStatusCode.InternalServerError,
                   isSuccess: false
                   );
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }


        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetProduct(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _response.BuildResponse(statusCode: HttpStatusCode.BadRequest,
                        isSuccess: false,
                        errors: new List<string>() { "Id cannot be less or equal to 0" });
                    return BadRequest(_response);
                }

                var product = await _dbProduct.GetAsync(u => u.Id == id);
                if (product == null)
                {
                    _response.BuildResponse(statusCode: HttpStatusCode.NotFound,
                        isSuccess: false,
                        errors: new List<string>() { $"Product with id {id} not found" });
                    return NotFound(_response);
                }
                _response.BuildResponse(statusCode: HttpStatusCode.OK,
                        isSuccess: true,
                        result: _mapper.Map<ProductDTO>(product));
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.BuildResponse(errors: new List<string>() { ex.ToString() },
                    isSuccess: false,
                    statusCode: HttpStatusCode.InternalServerError);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }
    }
}
