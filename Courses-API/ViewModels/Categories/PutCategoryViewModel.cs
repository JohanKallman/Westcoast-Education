using System.ComponentModel.DataAnnotations;

namespace Courses_API.ViewModels.Categories
{
  public class PutCategoryViewModel
  {
    [Required]
    public string? Name { get; set; }
  }
}