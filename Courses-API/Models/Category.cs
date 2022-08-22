namespace Courses_API.Models
{
  public class Category
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<TeacherCompetence> Teachers { get; set; } = new List<TeacherCompetence>();

  }
}