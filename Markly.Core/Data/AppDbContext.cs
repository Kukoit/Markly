using Markly.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Markly.Core.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // No DbSet<User> here: IdentityDbContext already provides Users
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookmarkTag> BookmarkTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);   // sets up the Identity tables

            modelBuilder.Entity<BookmarkTag>()
                .HasKey(bt => new { bt.BookmarkId, bt.TagId });

            // Maximum lengths for text columns
            modelBuilder.Entity<Bookmark>(entity =>
            {
                entity.Property(b => b.Url).HasMaxLength(2048);
                entity.Property(b => b.Title).HasMaxLength(200);
                entity.Property(b => b.Description).HasMaxLength(1000);
            });

            modelBuilder.Entity<Collection>()
                .Property(c => c.Name).HasMaxLength(100);

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(t => t.Name).HasMaxLength(50);
                entity.Property(t => t.Color).HasMaxLength(7);
            });
        }
    }
}