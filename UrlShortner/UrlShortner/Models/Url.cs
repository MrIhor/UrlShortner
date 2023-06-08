using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortner.Models
{
    public class Url
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(30)]
        public string BaseUrl { get; set; } = "";
        public string ShortUrl { get; set; } = "";
        public DateTime CreatedDate { get; set; }
    }
}

