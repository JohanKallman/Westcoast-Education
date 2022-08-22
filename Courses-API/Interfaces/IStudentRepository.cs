using Courses_API.Models;
using Courses_API.ViewModels.Student;

namespace Courses_API.Interfaces
{
  public interface IStudentRepository
  {
    public Task<List<StudentViewModel>> ListAllStudentsAsync();
    public Task<StudentViewModel?> ListStudentWithCoursesAsync(string id);
    public Task AddCourseToStudent(PostStudentCourseViewModel model);
    public Task RemoveStudentFromCourseAsync(int id);

    public Task AddNewStudentAsync(RegisterStudentViewModel model);
    public Task UpdateStudentAsync(string id, PutStudentViewModel model);
    public Task DeleteStudentAsync(string id);
    public Task<bool> SaveAllAsync();

  }
}