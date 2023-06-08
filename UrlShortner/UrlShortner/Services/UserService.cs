using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using UrlShortner.Data;
using UrlShortner.Dtos;
using UrlShortner.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace UrlShortner.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _db;
        private readonly IConfiguration _configuration;


        public UserService(DataContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        private string CreateJwtToken(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var credentials = new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256
            );

            var userClaims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddMinutes(20),
                notBefore: DateTime.UtcNow
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
        public async Task<ServiceResponse<User>> RegisterUser(RegisterDto user)
        {
            var serviceResponse = new ServiceResponse<User>();
            if (string.IsNullOrEmpty(user.Username) ||
            string.IsNullOrEmpty(user.Password) ||
            string.IsNullOrEmpty(user.Email))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Username or password is missing.";
                return serviceResponse;
            }

            User newUser = new User()
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Role = "Member"
            };

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();

            serviceResponse.Data = newUser;

            return serviceResponse;
        }

        public async Task<ServiceResponse<TokenDto>> GetAuthToken(LoginDto login)
        {
            var serviceResponse = new ServiceResponse<TokenDto>();
            if (string.IsNullOrEmpty(login.Username) ||
            string.IsNullOrEmpty(login.Password))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Username or password is missing.";

                return serviceResponse;
            }
            User user = await _db.Users
                .Where(u => u.Username.ToLower() == login.Username.ToLower() &&
                u.Password == login.Password).FirstOrDefaultAsync();

            if (user is not null)
            {
                var token = new TokenDto
                {
                    AccessToken = CreateJwtToken(user)
                };

                serviceResponse.Data = token;
                return serviceResponse;
            }
            serviceResponse.Success = false;
            serviceResponse.Message = "User is not found";

            return serviceResponse;
        }

        public Task<ServiceResponse<User>> GetUserFromToken(TokenDto token)
        {
            var serviceResponse = new ServiceResponse<User>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadToken(token.AccessToken) as JwtSecurityToken;

            if (jwtSecurityToken != null)
            {
                var usernameClaim = jwtSecurityToken.Claims;

                if (usernameClaim != null)
                {
                    var user = new User()
                    {
                        Username = usernameClaim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                        Email = usernameClaim.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                        Role = usernameClaim.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
                    };

                    serviceResponse.Data = user;
                }
            }

            return Task.FromResult(serviceResponse);
        }
    }
}