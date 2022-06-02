namespace ProgresiveBlog.API.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    [ApiController]
    public class PostController: Controller
    {
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPostById(int id)
        {
          
            return Ok();
        }
    }
}
