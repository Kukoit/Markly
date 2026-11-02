using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markly.Core.Data;
using Markly.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Markly.Core.Services
{
    public class CollectionService
    {
        private readonly AppDbContext _db;

        public CollectionService(AppDbContext db)
        {
            _db = db;
        }

        // All collections of one user, sorted by name
        public async Task<List<Collection>> GetCollectionsAsync(string userId)
        {
            return await _db.Collections
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        // One collection, but only if it belongs to this user (otherwise null)
        public async Task<Collection?> GetCollectionAsync(int id, string userId)
        {
            return await _db.Collections
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }

        // Create a new collection for this user
        public async Task AddCollectionAsync(Collection collection, string userId)
        {
            collection.UserId = userId;
            collection.ParentId = null;   // flat for now, no nesting

            _db.Collections.Add(collection);
            await _db.SaveChangesAsync();
        }

        // Rename a collection. Returns false if it doesn't exist or isn't this user's
        public async Task<bool> RenameCollectionAsync(int id, string newName, string userId)
        {
            var existing = await GetCollectionAsync(id, userId);
            if (existing == null)
            {
                return false;
            }

            existing.Name = newName;
            await _db.SaveChangesAsync();
            return true;
        }

        // Delete a collection. Its bookmarks are kept, they just no longer belong to a collection
        public async Task<bool> DeleteCollectionAsync(int id, string userId)
        {
            var existing = await GetCollectionAsync(id, userId);
            if (existing == null)
            {
                return false;
            }

            var bookmarks = await _db.Bookmarks
                .Where(b => b.CollectionId == id && b.UserId == userId)
                .ToListAsync();

            foreach (var bookmark in bookmarks)
            {
                bookmark.CollectionId = null;
            }

            _db.Collections.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}