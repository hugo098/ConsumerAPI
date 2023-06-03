using Microsoft.AspNetCore.Mvc;

namespace ConsumerAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/newversion")]
    [ApiController]
    [ApiVersion("2.0")]
    public class NewVersionController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> GetPosts()
        {
            return Ok("Hello world");

        }
    }
}
