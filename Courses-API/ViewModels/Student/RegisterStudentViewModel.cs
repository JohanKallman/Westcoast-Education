using System.ComponentModel.DataAnnotations;

namespace Courses_API.ViewModels.Student
{
  public class RegisterStudentViewModel
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Felaktig e-post adress")]
    public string? Email { get; set; }
  }
}