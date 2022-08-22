using System.ComponentModel.DataAnnotations.Schema;
using Courses_API.Models;
using Courses_API.ViewModels;

namespace Courses_API
{
  public class PostStudentCourseViewModel
  {
    public int CourseId { get; set; }
    public string? StudentId { get; set; }

    // [ForeignKey("CourseId")]
    // // public Course? Course { get; set; }
    // public CourseViewModel Course { get; set; } = new CourseViewModel();

    // [ForeignKey("StudentId")]
    // // public Student? Student { get; set; }
    // public StudentViewModel Student { get; set; } = new StudentViewModel();
  }
}