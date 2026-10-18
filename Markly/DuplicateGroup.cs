using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    // Result of duplicate detection, not saved to the database
    public class DuplicateGroup
    {
        public string ContentHash { get; set; }
        public List<Guid> BookmarkIds { get; set; } = new List<Guid>();
    }
}
