using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Splitify.Statistics.Api.Infrastructure;

namespace Splitify.Statistics.Api.Controllers
{
    [ApiController]
    [Route("api/v1/link")]
    public class LinkController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LinkController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] params string[] ids)
        {
            var links = await _context.Links
                .AsNoTracking()
                .Where(link => ids.Contains(link.Id))
                .ToListAsync();

            return Ok(links);
        }
    }
}