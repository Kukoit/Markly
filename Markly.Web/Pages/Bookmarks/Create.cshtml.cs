using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Bookmarks
{
    public class CreateModel : PageModel
    {
        private readonly BookmarkService _bookmarkService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public CreateModel(BookmarkService bookmarkService, UserManager<Markly.Core.Models.User> userManager)
        {
            _bookmarkService = bookmarkService;
            _userManager = userManager;
        }

        [BindProperty]
        public BookmarkInput Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();   // show the form again, with error messages
            }

            var bookmark = new Bookmark
            {
                Url = Input.Url,
                Title = Input.Title,
                Description = Input.Description ?? string.Empty,
                IsFavorite = Input.IsFavorite
            };

            var userId = _userManager.GetUserId(User)!;
            await _bookmarkService.AddBookmarkAsync(bookmark, userId);

            return RedirectToPage("Index");
        }
    }
}