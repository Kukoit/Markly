using Markly.Core.Models;
using Markly.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markly.Web.Pages.Collections
{
    public class IndexModel : PageModel
    {
        private readonly CollectionService _collectionService;
        private readonly UserManager<Markly.Core.Models.User> _userManager;

        public IndexModel(CollectionService collectionService, UserManager<Markly.Core.Models.User> userManager)
        {
            _collectionService = collectionService;
            _userManager = userManager;
        }

        public List<Collection> Collections { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            Collections = await _collectionService.GetCollectionsAsync(userId);
        }
    }
}