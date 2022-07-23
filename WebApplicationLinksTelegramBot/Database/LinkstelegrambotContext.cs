using Microsoft.EntityFrameworkCore;
using WebApplicationLinksTelegramBot.Authentication;
using WebApplicationLinksTelegramBot.Models;

namespace WebApplicationLinksTelegramBot.Database
{
    public partial class LinkstelegrambotContext : DbContext
    {
        public LinkstelegrambotContext()
        {
        }

        public LinkstelegrambotContext(DbContextOptions<LinkstelegrambotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auth> Auths { get; set; } = null!;
        public virtual DbSet<Link> Links { get; set; } = null!;

        public AuthenticatedUser AuthenticatedUser { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user=root;port=3306;database=linkstelegrambot", ServerVersion.Parse("10.4.11-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Auth>(entity =>
            {
                entity.ToTable("auths");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.ToTable("links");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
