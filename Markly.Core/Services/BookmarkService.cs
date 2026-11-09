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

        // All bookmarks of one user, newest first
        public async Task<List<Bookmark>> GetBookmarksAsync(string userId)
        {
            return await _db.Bookmarks
                .Include(b => b.Collection)          // NEW: also load the collection, to show its name
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        // One bookmark, but only if it belongs to this user (otherwise null)
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
            bookmark.CollectionId = await CheckCollectionAsync(bookmark.CollectionId, userId);   // NEW

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
            existing.IsFavorite = changed.IsFavorite;
            existing.CollectionId = await CheckCollectionAsync(changed.CollectionId, userId);   // NEW (back again)

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

        // NEW: only accept a collection that belongs to this user; otherwise use "no collection"
        private async Task<int?> CheckCollectionAsync(int? collectionId, string userId)
        {
            if (collectionId == null)
            {
                return null;
            }

            var belongsToUser = await _db.Collections
                .AnyAsync(c => c.Id == collectionId && c.UserId == userId);

            return belongsToUser ? collectionId : null;
        }
    }
}