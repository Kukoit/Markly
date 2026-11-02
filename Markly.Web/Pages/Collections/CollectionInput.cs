using System.ComponentModel.DataAnnotations;

namespace Markly.Web.Pages.Collections
{
    public class CollectionInput
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}