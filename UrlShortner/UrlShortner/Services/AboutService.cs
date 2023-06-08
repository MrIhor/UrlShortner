using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Data;
using UrlShortner.Models;

namespace UrlShortner.Services
{
    public class AboutService : IAboutService
    {
        private readonly DataContext _db;

        public AboutService(DataContext db)
        {
            _db = db;
        }

        public async Task<About> EditDescription(About newAbout)
        {
            _db.Description.Update(newAbout);
            await _db.SaveChangesAsync();
            About updatedAbout = await _db.Description.FirstOrDefaultAsync(d => d.Id == 1);
            return updatedAbout;
        }

        public async Task<About> GetDescription()
        {
            About description = await _db.Description.FirstOrDefaultAsync(d => d.Id == 1);
            return description;
        }
    }
}