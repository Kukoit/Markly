using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class Bookmark
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FaviconUrl { get; set; }
        public string PreviewImageUrl { get; set; }
        public Guid? CollectionId { get; set; }
        public List<Guid> TagIds { get; set; } = new List<Guid>();
        public bool IsFavorite { get; set; } = false;
        public string ContentHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public LinkHealth LinkHealth { get; set; } = new LinkHealth();

        // nice to haves
        public Reminder Reminder { get; set; }
        public FullTextContent FullTextContent { get; set; }
        public SiteMonitoring SiteMonitoring { get; set; }
        public List<AutoTagSuggestion> AutoTagSuggestions { get; set; } = new List<AutoTagSuggestion>();
    }
}
