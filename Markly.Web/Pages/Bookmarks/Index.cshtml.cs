using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Bookmarks
{
    public class IndexModel : PageModel
    {
        private readonly BookmarkService _bookmarkService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public IndexModel(BookmarkService bookmarkService, UserManager<Markly.Core.Models.User> userManager)
        {
            _bookmarkService = bookmarkService;
            _userManager = userManager;
        }

        public List<Bookmark> Bookmarks { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            Bookmarks = await _bookmarkService.GetBookmarksAsync(userId);
        }
    }
}