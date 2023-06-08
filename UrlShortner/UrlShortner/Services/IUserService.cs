using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortner.Dtos;
using UrlShortner.Models;

namespace UrlShortner.Services
{
    public interface IUserService
    {
        public Task<ServiceResponse<User>> RegisterUser(RegisterDto user);
        public Task<ServiceResponse<TokenDto>> GetAuthToken(LoginDto login);
        public Task<ServiceResponse<User>> GetUserFromToken(TokenDto token);
    }
}