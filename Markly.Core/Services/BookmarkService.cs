using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markly.Core.Data;
using Markly.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Markly.Core.Services
{
    public class BookmarkService
    {
        private readonly AppDbContext _db;

        public BookmarkService(AppDbContext db)
        {
            _db = db;
        }

        // All bookmarks of one user, newest on top
        public async Task<List<Bookmark>> GetBookmarksAsync(string userId)
        {
            return await _db.Bookmarks
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        // One bookmark, but only if it belongs to this user 
        public async Task<Bookmark?> GetBookmarkAsync(int id, string userId)
        {
            return await _db.Bookmarks
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        }

        // Save a new bookmark for this user
        public async Task AddBookmarkAsync(Bookmark bookmark, string userId)
        {
            bookmark.UserId = userId;
            bookmark.CreatedAt = DateTime.UtcNow;

            _db.Bookmarks.Add(bookmark);
            await _db.SaveChangesAsync();
        }

        // Change an existing bookmark. Returns false if it doesn't exist or isn't this user's
        public async Task<bool> UpdateBookmarkAsync(Bookmark changed, string userId)
        {
            var existing = await GetBookmarkAsync(changed.Id, userId);
            if (existing == null)
            {
                return false;
            }

            existing.Url = changed.Url;
            existing.Title = changed.Title;
            existing.Description = changed.Description;
            existing.CollectionId = changed.CollectionId;
            existing.IsFavorite = changed.IsFavorite;

            await _db.SaveChangesAsync();
            return true;
        }

        // Delete a bookmark. Returns false if it doesn't exist or isn't this user's
        public async Task<bool> DeleteBookmarkAsync(int id, string userId)
        {
            var existing = await GetBookmarkAsync(id, userId);
            if (existing == null)
            {
                return false;
            }

            _db.Bookmarks.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}