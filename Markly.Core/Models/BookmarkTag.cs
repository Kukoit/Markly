using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly.Core.Models
{
    public class BookmarkTag
    {
        public int BookmarkId { get; set; }
        public int TagId { get; set; }

        // Relationships
        public Bookmark Bookmark { get; set; }
        public Tag Tag { get; set; }
    }
}
