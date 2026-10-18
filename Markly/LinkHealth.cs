using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class LinkHealth
    {
        public string Status { get; set; } = "unknown"; // "ok", "broken", "redirect", "unknown"
        public int StatusCode { get; set; }
        public DateTime? LastCheckedAt { get; set; }
        public string RedirectUrl { get; set; }
    }
}
