using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Collections
{
    public class EditModel : PageModel
    {
        private readonly CollectionService _collectionService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public EditModel(CollectionService collectionService, UserManager<Markly.Core.Models.User> userManager)
        {
            _collectionService = collectionService;
            _userManager = userManager;
        }

        [BindProperty]
        public CollectionInput Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var collection = await _collectionService.GetCollectionAsync(id, userId);

            if (collection == null)
            {
                return NotFound();
            }

            Input = new CollectionInput
            {
                Name = collection.Name
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = _userManager.GetUserId(User)!;
            var success = await _collectionService.RenameCollectionAsync(id, Input.Name, userId);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}