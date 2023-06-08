using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Data;

namespace UrlShortner.Controllers
{
    [ApiController]
    [Route("{*path}")]
    public class RedirectController : ControllerBase
    {
        private readonly DataContext _db;

        public RedirectController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> RedirectToUrl()
        {
            var path = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{HttpContext.Request.Path.ToUriComponent().Trim('/')}";
            var urlMatch = await _db.Urls.FirstOrDefaultAsync(x =>
                x.ShortUrl.Trim() == path.Trim());

            if (urlMatch is null)
                return BadRequest("Invalid request.");

            return Redirect(urlMatch.BaseUrl);
        }
    }
}

