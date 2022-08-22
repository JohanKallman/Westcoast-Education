using System.ComponentModel;

namespace AdminMvc.ViewModels
{
  public class StudentPostCourseViewModel
  {
    [DisplayName("KursID")]
    public int CourseId { get; set; }
    [DisplayName("StudentID")]
    public string? StudentId { get; set; }

  }
}