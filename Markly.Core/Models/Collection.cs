using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly.Core.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }   

        // Relationships
        public User User { get; set; }
        public Collection Parent { get; set; }
        public List<Collection> Children { get; set; } = new();
        public List<Bookmark> Bookmarks { get; set; } = new();
    }
}
