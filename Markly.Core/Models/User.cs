using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relationships
        public List<Bookmark> Bookmarks { get; set; } = new();
        public List<Collection> Collections { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }
}
