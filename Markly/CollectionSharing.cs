using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class CollectionSharing
    {
        public bool IsShared { get; set; } = false;
        public string ShareToken { get; set; }
        public string Permission { get; set; } = "view"; // "view" or "edit"
        public List<Guid> SharedWithUserIds { get; set; } = new List<Guid>();
    }
}
