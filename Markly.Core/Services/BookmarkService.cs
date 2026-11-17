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

        // All bookmarks of one user, newest first.
        // Optional filters: only a certain tag, only favorites, a search text
        public async Task<List<Bookmark>> GetBookmarksAsync(string userId, int? tagId = null, bool favoritesOnly = false, string? search = null)
        {
            var query = _db.Bookmarks
                .Include(b => b.Collection)
                .Where(b => b.UserId == userId);

            if (tagId != null)
            {
                query = query.Where(b => _db.BookmarkTags.Any(bt => bt.BookmarkId == b.Id && bt.TagId == tagId));
            }

            if (favoritesOnly)
            {
                query = query.Where(b => b.IsFavorite);
            }

            if (!string.IsNullOrWhiteSpace(search))              // NEW
            {
                var pattern = $"%{search.Trim()}%";

                query = query.Where(b =>
                    EF.Functions.Like(b.Title, pattern) ||
                    EF.Functions.Like(b.Url, pattern) ||
                    EF.Functions.Like(b.Description, pattern));
            }

            return await query
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
            bookmark.CollectionId = await CheckCollectionAsync(bookmark.CollectionId, userId);

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
            existing.CollectionId = await CheckCollectionAsync(changed.CollectionId, userId);

            await _db.SaveChangesAsync();
            return true;
        }

        // NEW: switch favorite on/off. Returns false if it doesn't exist or isn't this user's
        public async Task<bool> ToggleFavoriteAsync(int id, string userId)
        {
            var existing = await GetBookmarkAsync(id, userId);
            if (existing == null)
            {
                return false;
            }

            existing.IsFavorite = !existing.IsFavorite;

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

        // Only accept a collection that belongs to this user; otherwise use "no collection"
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