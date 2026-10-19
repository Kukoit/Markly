using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly.Core.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CollectionId { get; set; }   
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relationships
        public User User { get; set; }
        public Collection Collection { get; set; }
        public List<BookmarkTag> BookmarkTags { get; set; } = new();
    }
}
