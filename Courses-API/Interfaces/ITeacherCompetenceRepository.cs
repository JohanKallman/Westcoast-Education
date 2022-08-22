using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses_API.Interfaces
{
  public interface ITeacherCompetenceRepository
  {
    public Task AddTeacherCompetenceAsync(PostTeacherCompetenceViewModel teacherCompetence);
    public Task DeleteTeacherCompetenceAsync(int id);
    public Task<bool> SaveAllAsync();
  }
}