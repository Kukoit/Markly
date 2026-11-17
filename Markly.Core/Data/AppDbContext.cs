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
        }
    }
}