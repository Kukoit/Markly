using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class BookmarkCollection
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; } // null = top-level, enables nesting
        public string Icon { get; set; }
        public string Color { get; set; }
        public int SortOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public CollectionSharing Sharing { get; set; }
    }
}
