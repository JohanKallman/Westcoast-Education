using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminMvc.ViewModels
{
  public class CoursePutViewModel
  {
    public int Id { get; set; }

    [RegularExpression(@"^\d{4}$", ErrorMessage = "välj 4 siffror")]
    [DisplayName("Kursnummer")]
    [Required(ErrorMessage = "Kursnummer är obligatoriskt")]
    public int CourseNo { get; set; }
    [DisplayName("Kurstitel")]
    [Required(ErrorMessage = "Titel är obligatoriskt")]
    public string? Name { get; set; }
    [DisplayName("Kurslängd")]
    [Required(ErrorMessage = "kurslängd är obligatoriskt")]
    public string? Duration { get; set; }
    [DisplayName("Längdenhet")]
    [Required(ErrorMessage = "Längdenhet är obligatoriskt")]
    public string? DurationUnit { get; set; }
    [DisplayName("Kategori")]
    [Required(ErrorMessage = "Kategori är obligatoriskt")]
    public string? Category { get; set; }
    [DisplayName("LärarId")]
    [Required(ErrorMessage = "LärarID är obligatoriskt")]
    public int TeacherId { get; set; }
    [DisplayName("Beskrivning")]
    [Required(ErrorMessage = "Beskrivning är obligatoriskt")]
    public string? Description { get; set; }
    [DisplayName("Detaljer")]
    [Required(ErrorMessage = "Detaljer är obligatoriskt")]
    public string? SubCourses { get; set; }

  }
}