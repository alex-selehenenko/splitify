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

        [HttpGet("{campaignId}")]
        public async Task<IActionResult> GetAsync(string campaignId)
        {
            var links = await _context.Links
                .AsNoTracking()
                .Where(link => link.CampaignId == campaignId)
                .ToListAsync();

            return Ok(links);
        }
    }
}