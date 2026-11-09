using System.ComponentModel.DataAnnotations;

namespace Markly.Web.Pages.Bookmarks
{
    public class BookmarkInput
    {
        [Required]
        [Url]
        public string Url { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Display(Name = "Favorite")]
        public bool IsFavorite { get; set; }

        [Display(Name = "Collection")]
        public int? CollectionId { get; set; }
    }
}