using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Models;
using UrlShortner.Services;
using System.Text;
using UrlShortner.Dtos;

namespace UrlShortner.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterDto user)
        {
            return Ok(await _userService.RegisterUser(user));
        }

        [HttpGet("GetToken")]
        public async Task<IActionResult> GetLoginToken(LoginDto login)
        {
            var token = await _userService.GetAuthToken(login);

            if (!token.Success)
            {
                return Ok(token);
            }

            return Ok(token);
        }
    }
}