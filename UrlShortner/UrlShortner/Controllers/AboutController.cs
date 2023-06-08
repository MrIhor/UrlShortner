using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Models;
using UrlShortner.Services;

namespace UrlShortner.Controllers
{
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet("GetAbout"), AllowAnonymous]
        public async Task<IActionResult> GetAbout()
        {
            return Ok(await _aboutService.GetDescription());
        }

        [HttpPost("UpdateAbout"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> UpdateAbout(About about)
        {
            return Ok(await _aboutService.EditDescription(about));
        }
    }
}