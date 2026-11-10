using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markly.Core.Data;
using Markly.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Markly.Core.Services
{
    public class TagService
    {
        private readonly AppDbContext _db;

        public TagService(AppDbContext db)
        {
            _db = db;
        }

        // ---------- Managing tags ----------

        // All tags of one user, sorted by name
        public async Task<List<Tag>> GetTagsAsync(string userId)
        {
            return await _db.Tags
                .Where(t => t.UserId == userId)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        // One tag, but only if it belongs to this user (otherwise null)
        public async Task<Tag?> GetTagAsync(int id, string userId)
        {
            return await _db.Tags
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        // Does this user already have a tag with this name? (ignores upper/lower case)
        // exceptTagId: when renaming, the tag itself doesn't count as a duplicate
        public async Task<bool> NameExistsAsync(string name, string userId, int? exceptTagId = null)
        {
            var lowerName = name.Trim().ToLower();

            return await _db.Tags.AnyAsync(t =>
                t.UserId == userId &&
                t.Name.ToLower() == lowerName &&
                (exceptTagId == null || t.Id != exceptTagId));
        }

        // Create a new tag for this user
        public async Task AddTagAsync(Tag tag, string userId)
        {
            tag.UserId = userId;
            tag.Name = tag.Name.Trim();

            _db.Tags.Add(tag);
            await _db.SaveChangesAsync();
        }

        // Change name and color. Returns false if the tag doesn't exist or isn't this user's
        public async Task<bool> UpdateTagAsync(int id, string name, string color, string userId)
        {
            var existing = await GetTagAsync(id, userId);
            if (existing == null)
            {
                return false;
            }

            existing.Name = name.Trim();
            existing.Color = color;

            await _db.SaveChangesAsync();
            return true;
        }

        // Delete a tag. The database removes its links to bookmarks automatically; the bookmarks stay
        public async Task<bool> DeleteTagAsync(int id, string userId)
        {
            var existing = await GetTagAsync(id, userId);
            if (existing == null)
            {
                return false;
            }

            _db.Tags.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        // ---------- Tags on bookmarks ----------

        // The IDs of the tags on one bookmark (to tick the right checkboxes in the Edit form)
        public async Task<List<int>> GetTagIdsForBookmarkAsync(int bookmarkId, string userId)
        {
            return await _db.BookmarkTags
                .Where(bt => bt.BookmarkId == bookmarkId && bt.Bookmark.UserId == userId)
                .Select(bt => bt.TagId)
                .ToListAsync();
        }

        // For the bookmark list: for each bookmark ID, its tags
        public async Task<Dictionary<int, List<Tag>>> GetTagsByBookmarkAsync(string userId)
        {
            var links = await _db.BookmarkTags
                .Include(bt => bt.Tag)
                .Where(bt => bt.Tag.UserId == userId)
                .ToListAsync();

            return links
                .GroupBy(bt => bt.BookmarkId)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(bt => bt.Tag).OrderBy(t => t.Name).ToList());
        }

        // Set exactly these tags on a bookmark: add the new ones, remove the unticked ones
        public async Task SetBookmarkTagsAsync(int bookmarkId, List<int> tagIds, string userId)
        {
            var bookmarkBelongsToUser = await _db.Bookmarks
                .AnyAsync(b => b.Id == bookmarkId && b.UserId == userId);

            if (!bookmarkBelongsToUser)
            {
                return;
            }

            // Only accept tags that belong to this user
            var validTagIds = await _db.Tags
                .Where(t => t.UserId == userId && tagIds.Contains(t.Id))
                .Select(t => t.Id)
                .ToListAsync();

            var oldLinks = await _db.BookmarkTags
                .Where(bt => bt.BookmarkId == bookmarkId)
                .ToListAsync();

            var oldTagIds = oldLinks.Select(bt => bt.TagId).ToList();

            // Remove links that are no longer ticked
            foreach (var link in oldLinks)
            {
                if (!validTagIds.Contains(link.TagId))
                {
                    _db.BookmarkTags.Remove(link);
                }
            }

            // Add links that are newly ticked
            foreach (var tagId in validTagIds)
            {
                if (!oldTagIds.Contains(tagId))
                {
                    _db.BookmarkTags.Add(new BookmarkTag { BookmarkId = bookmarkId, TagId = tagId });
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}