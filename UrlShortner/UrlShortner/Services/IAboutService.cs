using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortner.Models;

namespace UrlShortner.Services
{
    public interface IAboutService
    {
        public Task<About> GetDescription();
        public Task<About> EditDescription(About newAbout);
    }
}