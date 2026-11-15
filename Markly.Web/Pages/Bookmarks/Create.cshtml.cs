using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Markly.Web.Pages.Bookmarks
{
    public class CreateModel : PageModel
    {
        private readonly BookmarkService _bookmarkService;
        private readonly CollectionService _collectionService;
        private readonly TagService _tagService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public CreateModel(BookmarkService bookmarkService, CollectionService collectionService, TagService tagService, UserManager<Markly.Core.Models.User> userManager)
        {
            _bookmarkService = bookmarkService;
            _collectionService = collectionService;
            _tagService = tagService;
            _userManager = userManager;
        }

        [BindProperty]
        public BookmarkInput Input { get; set; } = new();

        // The entries for the collection dropdown
        public SelectList CollectionOptions { get; set; } = null!;

        // All tags of the user, for the checkboxes
        public List<Tag> Tags { get; set; } = new();

        public async Task OnGetAsync()
        {
            await LoadFormOptionsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadFormOptionsAsync();   // dropdown and checkboxes need their entries again
                return Page();
            }

            var bookmark = new Bookmark
            {
                Url = Input.Url,
                Title = Input.Title,
                Description = Input.Description ?? string.Empty,
                IsFavorite = Input.IsFavorite,
                CollectionId = Input.CollectionId
            };

            var userId = _userManager.GetUserId(User)!;
            await _bookmarkService.AddBookmarkAsync(bookmark, userId);

            // After saving, the bookmark has its Id, so the tags can be linked to it
            await _tagService.SetBookmarkTagsAsync(bookmark.Id, Input.TagIds, userId);

            return RedirectToPage("Index");
        }

        private async Task LoadFormOptionsAsync()
        {
            var userId = _userManager.GetUserId(User)!;

            var collections = await _collectionService.GetCollectionsAsync(userId);
            CollectionOptions = new SelectList(collections, "Id", "Name");

            Tags = await _tagService.GetTagsAsync(userId);
        }
    }
}