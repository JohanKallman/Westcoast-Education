using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses_API.Models
{
  public class Course
  {
    public int Id { get; set; }
    public int CourseNo { get; set; }
    public string? Name { get; set; }
    public string? Duration { get; set; }
    public string? DurationUnit { get; set; }
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = new Category();
    public string? Description { get; set; }
    public string? SubCourses { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public int TeacherId { get; set; }

    [ForeignKey("TeacherId")]
    public Teacher? Teacher { get; set; }








  }
}