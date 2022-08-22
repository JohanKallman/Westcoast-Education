namespace Courses_API.Models
{
  public class Teacher
  {
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();

    public ICollection<TeacherCompetence> Competences { get; set; } = new List<TeacherCompetence>();
  }
}