using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Models;

namespace UrlShortner.Data
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Url> Urls { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
    }
}

