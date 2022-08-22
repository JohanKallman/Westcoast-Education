using Courses_API.ViewModels;
using Courses_API.ViewModels.Course;

namespace Courses_API.Interfaces
{
  public interface ICourseRepository
  {
    public Task<List<CourseViewModel>> ListAllCoursesAsync();
    public Task<CourseViewModel?> GetCourseAsync(int id);
    public Task<PutCourseViewModel?> GetPutCourseAsync(int id);

    public Task<List<CourseViewModel>> GetCoursesByCategoryAsync(string category);
    public Task<CourseViewModel?> GetCourseByCourseNumberAsync(int id);
    public Task AddCourseAsync(PostCourseViewModel model);
    public Task DeleteCourseAsync(int id);
    public Task UpdateCourseAsync(PutCourseViewModel model, int id);

    public Task<bool> SaveAllAsync();

  }
}