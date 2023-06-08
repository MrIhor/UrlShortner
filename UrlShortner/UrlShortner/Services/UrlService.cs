using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Data;
using UrlShortner.Dtos;
using UrlShortner.Models;

namespace UrlShortner.Services
{
    public class UrlService : IUrlService
    {
        private readonly DataContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlService(DataContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public string ConvertToShort(UrlDto url)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz";
            var randomString = new string(Enumerable.Repeat(chars, 8)
                .Select(x => x[random.Next(x.Length)]).ToArray());

            return $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}/{randomString}";
        }

        public async Task<ServiceResponse<List<Url>>> AddShortUrl(UrlDto url)
        {
            var serviceResponse = new ServiceResponse<List<Url>>();
            var isExist = await _db.Urls.AnyAsync(u => u.BaseUrl == url.Url);

            try
            {
                if (Uri.TryCreate(url.Url, UriKind.Absolute, out var inputUri) && !isExist)
                {
                    Url urlResponse = new Url()
                    {
                        BaseUrl = url.Url,
                        ShortUrl = ConvertToShort(url),
                        CreatedDate = DateTime.UtcNow
                    };

                    await _db.Urls.AddAsync(urlResponse);
                    await _db.SaveChangesAsync();
                    serviceResponse.Data = await _db.Urls.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Url>>> DeleteUrl(int id)
        {
            var serviceResponse = new ServiceResponse<List<Url>>();

            try
            {
                var url = await _db.Urls.FirstAsync(u => u.Id == id);

                if (url is not null)
                {
                    _db.Urls.Remove(url);
                    await _db.SaveChangesAsync();
                }

                serviceResponse.Data = await _db.Urls.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Url>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<Url>>();
            var urls = await _db.Urls.ToListAsync();
            serviceResponse.Data = urls;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Url>> GetUrlById(int id)
        {
            var serviceResponse = new ServiceResponse<Url>();
            try
            {
                var url = await _db.Urls.FirstAsync(u => u.Id == id);

                if (url is not null)
                {
                    serviceResponse.Data = url;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}

