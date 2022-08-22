namespace AdminMvc.ViewModels
{
  public class StudentViewModel
  {
    public string? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    //public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public ICollection<StudentCourseViewModel> StudentCourses { get; set; } = new List<StudentCourseViewModel>();
  }
}