
namespace UserMvc.ViewModels
{
  public class CourseViewModel
  {
    public int CourseId { get; set; }
    public int CourseNo { get; set; }
    public string? Name { get; set; }
    public string? Duration { get; set; }
    public string? DurationUnit { get; set; }
    public string? Category { get; set; }
    public string? Teacher { get; set; }
    public string? Description { get; set; }
    public string? SubCourses { get; set; }

  }
}