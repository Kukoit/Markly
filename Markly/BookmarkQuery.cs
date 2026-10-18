using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    // Used for search/filter requests, not saved to the database
    public class BookmarkQuery
    {
        public string SearchText { get; set; }
        public List<Guid> TagIds { get; set; } = new List<Guid>();
        public Guid? CollectionId { get; set; }
        public bool? FavoritesOnly { get; set; }
        public string LinkStatus { get; set; }
        public string SortBy { get; set; } = "createdAt";       // "createdAt", "title", "updatedAt"
        public string SortDirection { get; set; } = "desc";     // "asc" or "desc"
    }
}
