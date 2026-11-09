using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Collections
{
    public class DeleteModel : PageModel
    {
        private readonly CollectionService _collectionService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public DeleteModel(CollectionService collectionService, UserManager<Markly.Core.Models.User> userManager)
        {
            _collectionService = collectionService;
            _userManager = userManager;
        }

        public Collection Collection { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var collection = await _collectionService.GetCollectionAsync(id, userId);

            if (collection == null)
            {
                return NotFound();
            }

            Collection = collection;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var success = await _collectionService.DeleteCollectionAsync(id, userId);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}