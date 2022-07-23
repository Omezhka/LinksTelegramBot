using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinksTelegramBot.Database
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Link> Links { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost; user=root; port=3306; database=linkstelegrambot", new MySqlServerVersion(new Version(8, 0, 22)));
        }
    }
}
