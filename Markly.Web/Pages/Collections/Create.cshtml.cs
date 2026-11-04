using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Collections
{
    public class CreateModel : PageModel
    {
        private readonly CollectionService _collectionService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public CreateModel(CollectionService collectionService, UserManager<Markly.Core.Models.User> userManager)
        {
            _collectionService = collectionService;
            _userManager = userManager;
        }

        [BindProperty]
        public CollectionInput Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var collection = new Collection
            {
                Name = Input.Name
            };

            var userId = _userManager.GetUserId(User)!;
            await _collectionService.AddCollectionAsync(collection, userId);

            return RedirectToPage("Index");
        }
    }
}