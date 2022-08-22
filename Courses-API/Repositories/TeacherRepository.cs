using AutoMapper;
using AutoMapper.QueryableExtensions;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels.Teacher;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
  public class TeacherRepository : ITeacherRepository
  {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public TeacherRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task AddNewTeacherAsync(PostTeacherViewModel model)
    {
      var teacher = new Teacher
      {
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        Address = model.Address,
        PhoneNumber = model.PhoneNumber
      };

      await _context.Teachers.AddAsync(teacher);
    }

    public async Task DeleteTeacherAsync(int id)
    {

      var response = await _context.Teachers.Where(t => t.Id == id).Include(c => c.Courses).FirstOrDefaultAsync();

      if (response is null)
      {
        throw new Exception($"Vi kunde inte hitta någon lärare med id {id}");
      }

      if (response.Courses.Count > 0)
      {
        throw new Exception($"Kan ej radera läraren med id: {id} när denne står skriven på kurser");
      }

      _context.Teachers.Remove(response);

    }

    public async Task UpdateTeacherAsync(int id, PutTeacherViewModel model)
    {
      var teacher = await _context.Teachers.FindAsync(id);

      if (teacher is null) throw new Exception($"Kunde inte hitta läraren med id {id} i vårt system");
      teacher.Id = model.Id;
      teacher.FirstName = model.FirstName;
      teacher.LastName = model.LastName;
      teacher.Email = model.Email;
      teacher.Address = model.Address;
      teacher.PhoneNumber = model.PhoneNumber;

      _context.Teachers.Update(teacher);
    }

    public async Task<List<TeacherViewModel>> GetTeachersByCompetenceAsync(string competence)
    {
      var competences = await _context.Categories.ToListAsync();
      bool matchFound = false;
      foreach (var comp in competences)
      {
        if (competence.ToLower() == comp.Name!.ToLower())
        {
          matchFound = true;
          break;
        }
      }
      if (matchFound == false)
      {
        throw new Exception("Kategori finns inte i systemet");
      }

      var teachers = await _context.Teachers
      .Include(t => t.Competences)
      .ThenInclude(tc => tc.Competence)
      .ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).ToListAsync();

      var teachersToDisplay = new List<TeacherViewModel>();

      foreach (var teacher in teachers)
      {
        foreach (var c in teacher.Competences)
        {
          if (c.Competence!.Name!.ToLower() == competence.ToLower())
          {
            teachersToDisplay.Add(teacher);
          }
        }
      }
      return teachersToDisplay;
    }

    public async Task<List<TeacherViewModel>> ListAllTeachersAsync()
    {

      return await _context.Teachers
      .ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).ToListAsync();

    }

    public async Task AddCompetenceToTeacher(PostTeacherCompetenceViewModel model)
    {
      var teacherToCheck = await _context.Teachers.FindAsync(model.TeacherId);
      var competenceToCheck = await _context.Categories.FindAsync(model.CompetenceId);

      if (teacherToCheck is null)
      {
        throw new Exception($"Läraren med id: {model.TeacherId} finns inte i systemet");
      }

      if (competenceToCheck is null)
      {
        throw new Exception($"Kompetensen med id: {model.CompetenceId} finns inte i systemet");
      }

      var allTeacherCompetences = await _context.TeacherCompetences.ToListAsync();


      var addTeacherCompetence = new TeacherCompetence();
      _mapper.Map<PostTeacherCompetenceViewModel, TeacherCompetence>(model, addTeacherCompetence);


      // var teacherCompetence = new TeacherCompetence
      // {
      //   TeacherId = model.TeacherId,
      //   CompetenceId = model.CompetenceId
      // };

      foreach (var teacherComps in allTeacherCompetences)
      {
        if (teacherComps.TeacherId == addTeacherCompetence.TeacherId && teacherComps.CompetenceId == addTeacherCompetence.CompetenceId)
        {
          throw new Exception($"Läraren har redan denna kurs som kompetens");
        }
      }

      await _context.TeacherCompetences.AddAsync(addTeacherCompetence);
    }

    public async Task<TeacherViewModel?> ListATeacherWithCompetencesAsync(int id)
    {

      return await _context.Teachers.Where(t => t.Id == id)
      .Include(t => t.Competences)
      .ThenInclude(tc => tc.Competence)
      .ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}