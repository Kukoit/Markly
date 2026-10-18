using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string AuthProvider { get; set; } // "password", "google"
        public DateTime CreatedAt { get; set; }
        public UserSettings Settings { get; set; } = new UserSettings();
    }
}
