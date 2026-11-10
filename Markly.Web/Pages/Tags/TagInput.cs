using System.ComponentModel.DataAnnotations;

namespace Markly.Web.Pages.Tags
{
    public class TagInput
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^#[0-9a-fA-F]{6}$", ErrorMessage = "Please choose a valid color.")]
        public string Color { get; set; } = "#6c757d";
    }
}