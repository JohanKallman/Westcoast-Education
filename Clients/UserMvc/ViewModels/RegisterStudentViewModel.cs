using System.ComponentModel.DataAnnotations;

namespace UserMvc.ViewModels
{
  public class RegisterStudentViewModel
  {
    [Display(Name = "Förnamn")]
    [Required(ErrorMessage = "Förnamn Obligatoriskt")]
    public string? FirstName { get; set; }

    [Display(Name = "Efternamn")]
    [Required(ErrorMessage = "Efternamn Obligatoriskt")]
    public string? LastName { get; set; }

    [Display(Name = "Adress")]
    [Required(ErrorMessage = "Adress Obligatoriskt")]
    public string? Address { get; set; }

    [Display(Name = "Telefon")]
    [Required(ErrorMessage = "Telefon Obligatoriskt")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Epost")]
    [Required(ErrorMessage = "Epost Obligatoriskt")]
    public string? Email { get; set; }
  }
}