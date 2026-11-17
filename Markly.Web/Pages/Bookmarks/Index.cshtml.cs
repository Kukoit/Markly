using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
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

        // The tag the list is filtered by (null = show all)
        public Tag? FilterTag { get; set; }

        public async Task OnGetAsync(int? tagId)
        {
            var userId = _userManager.GetUserId(User)!;

            Bookmarks = await _bookmarkService.GetBookmarksAsync(userId, tagId);
            TagsByBookmark = await _tagService.GetTagsByBookmarkAsync(userId);

            if (tagId != null)
            {
                FilterTag = await _tagService.GetTagAsync(tagId.Value, userId);
            }
        }
    }
}