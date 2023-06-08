using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Dtos;
using UrlShortner.Models;
using UrlShortner.Services;

namespace UrlShortner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("GetAll"), AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<Url>>>> GetAllUrls()
        {
            return Ok(await _urlService.GetAll());
        }

        [HttpGet("{id}"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Member")]
        public async Task<ActionResult<ServiceResponse<Url>>> GetUrl(int id)
        {
            return Ok(await _urlService.GetUrlById(id));
        }

        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Member")]
        public async Task<ActionResult<ServiceResponse<List<Url>>>> AddUrl(UrlDto url)
        {
            return Ok(await _urlService.AddShortUrl(url));
        }

        [HttpDelete("{id}"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Member")]
        public async Task<ActionResult<ServiceResponse<List<Url>>>> DeleteUrl(int id)
        {
            var response = await _urlService.DeleteUrl(id);

            if (response.Data is null)
                return NotFound(response);

            return Ok(response);
        }
    }
}


