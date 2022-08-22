using System.ComponentModel.DataAnnotations;

namespace AdminMvc.ViewModels
{
  public class StudentPostViewModel
  {
    [Required(ErrorMessage = "Förnamn är obligatoriskt")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Efternamn är obligatoriskt")]
    public string? LastName { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Felaktig e-post adress")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Telefon är obligatoriskt")]
    public string? PhoneNumber { get; set; }
    [Required(ErrorMessage = "Adress är obligatoriskt")]
    public string? Address { get; set; }
  }
}