using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Tags
{
    public class EditModel : PageModel
    {
        private readonly TagService _tagService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public EditModel(TagService tagService, UserManager<Markly.Core.Models.User> userManager)
        {
            _tagService = tagService;
            _userManager = userManager;
        }

        [BindProperty]
        public TagInput Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var tag = await _tagService.GetTagAsync(id, userId);

            if (tag == null)
            {
                return NotFound();
            }

            Input = new TagInput
            {
                Name = tag.Name,
                Color = tag.Color
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            if (await _tagService.NameExistsAsync(Input.Name, userId, id))
            {
                ModelState.AddModelError("Input.Name", "You already have a tag with this name.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = await _tagService.UpdateTagAsync(id, Input.Name, Input.Color, userId);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}