using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly.Core.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        // Relationships
        public User User { get; set; }
        public List<BookmarkTag> BookmarkTags { get; set; } = new();
    }
}
