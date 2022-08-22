using Courses_API.ViewModels.Teacher;

namespace Courses_API.Interfaces
{
  public interface ITeacherRepository
  {
    public Task AddNewTeacherAsync(PostTeacherViewModel model);
    public Task<List<TeacherViewModel>> ListAllTeachersAsync();
    public Task AddCompetenceToTeacher(PostTeacherCompetenceViewModel model);
    public Task<List<TeacherViewModel>> GetTeachersByCompetenceAsync(string competence);
    public Task<TeacherViewModel?> ListATeacherWithCompetencesAsync(int id);
    public Task UpdateTeacherAsync(int id, PutTeacherViewModel model);
    public Task DeleteTeacherAsync(int id);
    public Task<bool> SaveAllAsync();

  }
}