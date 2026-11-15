using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Markly.Web.Pages.Bookmarks
{
    public class EditModel : PageModel
    {
        private readonly BookmarkService _bookmarkService;
        private readonly CollectionService _collectionService;
        private readonly TagService _tagService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public EditModel(BookmarkService bookmarkService, CollectionService collectionService, TagService tagService, UserManager<Markly.Core.Models.User> userManager)
        {
            _bookmarkService = bookmarkService;
            _collectionService = collectionService;
            _tagService = tagService;
            _userManager = userManager;
        }

        [BindProperty]
        public BookmarkInput Input { get; set; } = new();

        public SelectList CollectionOptions { get; set; } = null!;

        public List<Tag> Tags { get; set; } = new();

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
                IsFavorite = bookmark.IsFavorite,
                CollectionId = bookmark.CollectionId,
                TagIds = await _tagService.GetTagIdsForBookmarkAsync(id, userId)
            };

            await LoadFormOptionsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                await LoadFormOptionsAsync();
                return Page();
            }

            var changed = new Bookmark
            {
                Id = id,
                Url = Input.Url,
                Title = Input.Title,
                Description = Input.Description ?? string.Empty,
                IsFavorite = Input.IsFavorite,
                CollectionId = Input.CollectionId
            };

            var userId = _userManager.GetUserId(User)!;
            var success = await _bookmarkService.UpdateBookmarkAsync(changed, userId);

            if (!success)
            {
                return NotFound();
            }

            await _tagService.SetBookmarkTagsAsync(id, Input.TagIds, userId);

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