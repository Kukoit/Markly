using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Tags
{
    public class DeleteModel : PageModel
    {
        private readonly TagService _tagService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public DeleteModel(TagService tagService, UserManager<Markly.Core.Models.User> userManager)
        {
            _tagService = tagService;
            _userManager = userManager;
        }

        public Tag Tag { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var tag = await _tagService.GetTagAsync(id, userId);

            if (tag == null)
            {
                return NotFound();
            }

            Tag = tag;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var success = await _tagService.DeleteTagAsync(id, userId);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}