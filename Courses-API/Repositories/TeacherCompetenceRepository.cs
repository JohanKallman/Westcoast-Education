using AutoMapper;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
  public class TeacherCompetenceRepository : ITeacherCompetenceRepository
  {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public TeacherCompetenceRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }
    public async Task AddTeacherCompetenceAsync(PostTeacherCompetenceViewModel teacherCompetence)
    {

      var teacherToCheck = await _context.Teachers.FindAsync(teacherCompetence.TeacherId);
      var competenceToCheck = await _context.Categories.FindAsync(teacherCompetence.CompetenceId);

      if (teacherToCheck is null)
      {
        throw new Exception($"Läraren med id: {teacherCompetence.TeacherId} finns inte i systemet");
      }

      if (competenceToCheck is null)
      {
        throw new Exception($"Kompetensen med id: {teacherCompetence.CompetenceId} finns inte i systemet");
      }

      var allTeacherCompetences = await _context.TeacherCompetences.ToListAsync();

      var addTeacherCompetence = new TeacherCompetence();
      _mapper.Map<PostTeacherCompetenceViewModel, TeacherCompetence>(teacherCompetence, addTeacherCompetence);

      foreach (var teacherComps in allTeacherCompetences)
      {
        if (teacherComps.TeacherId == addTeacherCompetence.TeacherId && teacherComps.CompetenceId == addTeacherCompetence.CompetenceId)
        {
          throw new Exception($"Läraren har redan denna kurs som kompetens");
        }
      }

      await _context.TeacherCompetences.AddAsync(addTeacherCompetence);
    }

    public async Task DeleteTeacherCompetenceAsync(int id)
    {
      var response = await _context.TeacherCompetences.FindAsync(id);
      if (response is null)
      {
        throw new Exception($"Vi kunde inte hitta denna lärarkompetens med id {id}");
      }
      if (response is not null)
      {
        _context.TeacherCompetences.Remove(response);
      }
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}