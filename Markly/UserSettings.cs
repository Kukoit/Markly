using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class UserSettings
    {
        public string DefaultView { get; set; } = "grid";   // "grid" or "list"
        public string Theme { get; set; } = "system";       // "light", "dark", "system"
        public bool IsAutoTagEnabled { get; set; } = false;
    }
}
