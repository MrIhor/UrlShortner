using System;
using UrlShortner.Dtos;
using UrlShortner.Models;

namespace UrlShortner.Services
{
    public interface IUrlService
    {
        Task<ServiceResponse<List<Url>>> GetAll();
        Task<ServiceResponse<Url>> GetUrlById(int id);
        Task<ServiceResponse<List<Url>>> AddShortUrl(UrlDto url);
        Task<ServiceResponse<List<Url>>> DeleteUrl(int id);
    }

}

