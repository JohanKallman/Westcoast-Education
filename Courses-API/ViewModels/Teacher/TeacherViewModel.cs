using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.TeacherCompetence;

namespace Courses_API
{
  public class TeacherViewModel
  {
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public ICollection<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();
    public ICollection<TeacherCompetenceViewModel> Competences { get; set; } = new List<TeacherCompetenceViewModel>();

  }
}