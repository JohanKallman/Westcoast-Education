using System.ComponentModel.DataAnnotations;

namespace Courses_API.ViewModels.Categories
{
  public class PostCategoryViewModel
  {
    [Required(ErrorMessage = "Ämnesnamn är obligatoriskt")]
    public string? Name { get; set; }
  }
}