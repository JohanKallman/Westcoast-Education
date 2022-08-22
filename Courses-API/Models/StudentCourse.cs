using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses_API.Models
{
  public class StudentCourse
  {
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string? StudentId { get; set; }

    [ForeignKey("CourseId")]
    public Course? Course { get; set; }
    [ForeignKey("StudentId")]
    public Student? Student { get; set; }

    public bool IsActive { get; set; } = true;

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

  }
}