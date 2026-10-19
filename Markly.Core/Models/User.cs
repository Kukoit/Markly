using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;                 

namespace Markly.Core.Models
{
    public class User : IdentityUser                 // inherits from IdentityUser
    {
       
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relationships
        public List<Bookmark> Bookmarks { get; set; } = new();
        public List<Collection> Collections { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }
}