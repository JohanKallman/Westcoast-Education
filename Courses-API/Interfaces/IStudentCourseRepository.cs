using Courses_API.Models;

namespace Courses_API.Interfaces
{
  public interface IStudentCourseRepository
  {
    public Task AddStudentCourseAsync(PostStudentCourseViewModel studentCourse);
    public Task<List<StudentCourseViewModel>> ListAllStudentRegistrationsAsync();

    public Task DeleteStudentRegistrationRecordAsync(int id);
    public Task RemoveStudentFromCourseAsync(int id);
    public Task<bool> SaveAllAsync();
  }
}