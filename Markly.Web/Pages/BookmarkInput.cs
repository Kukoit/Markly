using System.ComponentModel.DataAnnotations;

namespace Markly.Web.Pages.Bookmarks
{
    public class BookmarkInput
    {
        [Required]
        [Url]
        [StringLength(2048)]
        public string Url { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Display(Name = "Favorite")]
        public bool IsFavorite { get; set; }

        [Display(Name = "Collection")]
        public int? CollectionId { get; set; }

        public List<int> TagIds { get; set; } = new();
    }
}