using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class SiteMonitoring
    {
        public bool IsEnabled { get; set; } = false;
        public string CheckFrequency { get; set; } = "weekly"; // "daily" or "weekly"
        public bool HasChangeDetected { get; set; } = false;
        public string LastSnapshotHash { get; set; }
        public DateTime? LastCheckedAt { get; set; }
    }
}
