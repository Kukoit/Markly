using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class ImportJob
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Source { get; set; } // "raindrop", "pocket", "html", "chrome", "other"
        public string Status { get; set; } = "pending"; // "pending", "processing", "completed", "failed"
        public int TotalItems { get; set; }
        public int ProcessedItems { get; set; } = 0;
        public bool ShouldPreserveNesting { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
