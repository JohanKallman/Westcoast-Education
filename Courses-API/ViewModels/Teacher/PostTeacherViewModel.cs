using System.ComponentModel.DataAnnotations;
using Courses_API.Models;

namespace Courses_API.ViewModels.Teacher
{
  public class PostTeacherViewModel
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Felaktig e-post adress")]
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }

  }
}