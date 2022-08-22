using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminMvc.ViewModels
{
  public class TeacherPutViewModel
  {
    public int Id { get; set; }

    [Required]
    [DisplayName("Förnamn")]
    public string? FirstName { get; set; }

    [Required]
    [DisplayName("Efternamn")]
    public string? LastName { get; set; }

    [Required]
    [DisplayName("Epost")]
    public string? Email { get; set; }

    [Required]
    [DisplayName("Adress")]
    public string? Address { get; set; }

    [Required]
    [DisplayName("Telefon")]
    public string? PhoneNumber { get; set; }

  }
}