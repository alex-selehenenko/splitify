using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Splitify.Identity.Api.Controllers
{
    [Route("api/v1/health")]
    [ApiController]
    public class Health : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
