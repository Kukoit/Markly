using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private readonly TagService _tagService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public IndexModel(TagService tagService, UserManager<Markly.Core.Models.User> userManager)
        {
            _tagService = tagService;
            _userManager = userManager;
        }

        public List<Tag> Tags { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            Tags = await _tagService.GetTagsAsync(userId);
        }
    }
}