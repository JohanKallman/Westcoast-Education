using System.ComponentModel.DataAnnotations.Schema;
using Courses_API.Models;
using Courses_API.ViewModels;

namespace Courses_API
{
  public class StudentCourseViewModel
  {
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string? StudentId { get; set; }

    [ForeignKey("CourseId")]
    // public Course? Course { get; set; }
    public CourseViewModel? Course { get; set; }


    [ForeignKey("StudentId")]
    // public Student? Student { get; set; }
    public StudentViewModel? Student { get; set; }

    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

  }
}