using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Bookmarks
{
    public class DeleteModel : PageModel
    {
        private readonly BookmarkService _bookmarkService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public DeleteModel(BookmarkService bookmarkService, UserManager<Markly.Core.Models.User> userManager)
        {
            _bookmarkService = bookmarkService;
            _userManager = userManager;
        }

        public Bookmark Bookmark { get; set; } = null!;

        // Show "really delete?"
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var bookmark = await _bookmarkService.GetBookmarkAsync(id, userId);

            if (bookmark == null)
            {
                return NotFound();
            }

            Bookmark = bookmark;
            return Page();
        }

        // Actually delete
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var success = await _bookmarkService.DeleteBookmarkAsync(id, userId);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}