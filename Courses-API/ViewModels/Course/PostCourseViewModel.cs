using System.ComponentModel.DataAnnotations;

namespace Courses_API.ViewModels
{
  public class PostCourseViewModel
  {
    [RegularExpression(@"^\d{4}$", ErrorMessage = "välj 4 siffror")]
    [Required(ErrorMessage = "Kursnummer är obligatoriskt")]
    public int CourseNo { get; set; }
    [Required(ErrorMessage = "Titel är obligatoriskt")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "kurslängd är obligatoriskt")]
    public string? Duration { get; set; }
    [Required(ErrorMessage = "Längdenhet är obligatoriskt")]
    public string? DurationUnit { get; set; }
    [Required(ErrorMessage = "Kategori är obligatoriskt")]
    public string? Category { get; set; }
    public int TeacherId { get; set; }
    public string? Description { get; set; }
    public string? SubCourses { get; set; }

  }
}