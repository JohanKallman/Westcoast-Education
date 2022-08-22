using System.ComponentModel.DataAnnotations.Schema;

namespace AdminMvc.ViewModels
{
  public class StudentCourseViewModel
  {
    public int Id { get; set; }
    public string? StudentId { get; set; }
    public int CourseId { get; set; }

    [ForeignKey("StudentId")]
    public StudentViewModel Student { get; set; } = new StudentViewModel();
    [ForeignKey("CourseId")]
    public CourseViewModel Course { get; set; } = new CourseViewModel();
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

  }
}