using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Bookmarks
{
    public class IndexModel : PageModel
    {
        private readonly BookmarkService _bookmarkService;
        private readonly TagService _tagService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public IndexModel(BookmarkService bookmarkService, TagService tagService, UserManager<Markly.Core.Models.User> userManager)
        {
            _bookmarkService = bookmarkService;
            _tagService = tagService;
            _userManager = userManager;
        }

        public List<Bookmark> Bookmarks { get; set; } = new();

        // For each bookmark ID: its tags
        public Dictionary<int, List<Tag>> TagsByBookmark { get; set; } = new();

        // The current filters (so the page can keep them when links are clicked)
        public int? TagId { get; set; }
        public bool FavoritesOnly { get; set; }

        // The tag the list is filtered by (null = no tag filter)
        public Tag? FilterTag { get; set; }

        public async Task OnGetAsync(int? tagId, bool favorites)
        {
            TagId = tagId;
            FavoritesOnly = favorites;

            var userId = _userManager.GetUserId(User)!;

            Bookmarks = await _bookmarkService.GetBookmarksAsync(userId, tagId, favorites);
            TagsByBookmark = await _tagService.GetTagsByBookmarkAsync(userId);

            if (tagId != null)
            {
                FilterTag = await _tagService.GetTagAsync(tagId.Value, userId);
            }
        }

        // Runs when the star button is clicked
        public async Task<IActionResult> OnPostToggleFavoriteAsync(int id, int? tagId, bool favorites)
        {
            var userId = _userManager.GetUserId(User)!;
            var success = await _bookmarkService.ToggleFavoriteAsync(id, userId);

            if (!success)
            {
                return NotFound();
            }

            // Back to the list, with the same filters as before
            return RedirectToPage(new { tagId, favorites });
        }
    }
}