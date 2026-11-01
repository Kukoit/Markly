using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Bookmarks
{
    public class EditModel : PageModel
    {
        private readonly BookmarkService _bookmarkService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public EditModel(BookmarkService bookmarkService, UserManager<Markly.Core.Models.User> userManager)
        {
            _bookmarkService = bookmarkService;
            _userManager = userManager;
        }

        [BindProperty]
        public BookmarkInput Input { get; set; } = new();

        // Load the bookmark and fill the form
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var bookmark = await _bookmarkService.GetBookmarkAsync(id, userId);

            if (bookmark == null)
            {
                return NotFound();
            }

            Input = new BookmarkInput
            {
                Url = bookmark.Url,
                Title = bookmark.Title,
                Description = bookmark.Description,
                IsFavorite = bookmark.IsFavorite
            };

            return Page();
        }

        // Save the changes
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var changed = new Bookmark
            {
                Id = id,
                Url = Input.Url,
                Title = Input.Title,
                Description = Input.Description ?? string.Empty,
                IsFavorite = Input.IsFavorite
            };

            var userId = _userManager.GetUserId(User)!;
            var success = await _bookmarkService.UpdateBookmarkAsync(changed, userId);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}