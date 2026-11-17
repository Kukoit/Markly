using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Tags
{
    public class CreateModel : PageModel
    {
        private readonly TagService _tagService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public CreateModel(TagService tagService, UserManager<Markly.Core.Models.User> userManager)
        {
            _tagService = tagService;
            _userManager = userManager;
        }

        [BindProperty]
        public TagInput Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // 1. First the normal validation (required, length, color format)
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = _userManager.GetUserId(User)!;

            // 2. Only then the duplicate check, now Input.Name is guaranteed not to be empty
            if (await _tagService.NameExistsAsync(Input.Name, userId))
            {
                ModelState.AddModelError("Input.Name", "You already have a tag with this name.");
                return Page();
            }

            var tag = new Tag
            {
                Name = Input.Name,
                Color = Input.Color
            };

            await _tagService.AddTagAsync(tag, userId);

            return RedirectToPage("Index");
        }
    }
}