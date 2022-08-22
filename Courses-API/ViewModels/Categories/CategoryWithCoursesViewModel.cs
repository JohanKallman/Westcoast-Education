namespace Courses_API.ViewModels.Categories
{
  public class CategoryWithCoursesViewModel
  {
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

  }
}