using AutoMapper;
using AutoMapper.QueryableExtensions;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Course;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
  public class CourseRepository : ICourseRepository
  {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public CourseRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task AddCourseAsync(PostCourseViewModel model)
    {
      var category = await _context.Categories
      .Where(c => c.Name!.ToLower() == model.Category!.ToLower())
      .SingleOrDefaultAsync();

      if (category is null)
      {
        throw new Exception($"Tyvärr så finns inte ämnet {model.Category} i systemet");
      }

      var teacher = await _context.Teachers
      .Where(t => t.Id == model.TeacherId)
      .Include(c => c.Competences)
      .SingleOrDefaultAsync();

      if (teacher is null)
      {
        throw new Exception($"Tyvärr så finns inte läraren med id: {model.TeacherId} i systemet");
      }

      var hasCompetence = false;

      foreach (var competence in teacher.Competences)
      {
        if (category.Id == competence.CompetenceId)
        {
          hasCompetence = true;
        }
      }

      if (hasCompetence == false)
      {
        throw new Exception("Tyvärr så har läraren inte rätt kompetenser för denna kurs");
      }

      var courseToAdd = _mapper.Map<Course>(model);
      courseToAdd.Category = category;
      courseToAdd.Teacher = teacher;
      await _context.Courses.AddAsync(courseToAdd);

    }

    public async Task DeleteCourseAsync(int id)
    {
      var response = await _context.Courses.FindAsync(id);
      if (response is null)
      {
        throw new Exception($"Vi kunde inte hitta någon kurs med id {id}");
      }
      if (response is not null)
      {
        _context.Courses.Remove(response);
      }
    }

    public async Task<CourseViewModel?> GetCourseAsync(int id)
    {
      return await _context.Courses.Where(c => c.Id == id)
      .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
      .SingleOrDefaultAsync();
    }

    public async Task<PutCourseViewModel?> GetPutCourseAsync(int id)
    {
      var course = await _context.Courses.FindAsync(id);


      var teacher = await _context.Teachers.FindAsync(course!.TeacherId);

      var category = await _context.Categories.FindAsync(course.CategoryId);

      var coursePutViewModel = new PutCourseViewModel
      {
        Id = course.Id,
        CourseNo = course.CourseNo,
        Name = course.Name,
        Duration = course.Duration,
        DurationUnit = course.DurationUnit,
        Category = category!.Name,
        TeacherId = teacher!.Id,
        Description = course.Description,
        SubCourses = course.SubCourses
      };
      return coursePutViewModel;
    }

    public async Task<CourseViewModel?> GetCourseByCourseNumberAsync(int id)
    {
      return await _context.Courses.Where(c => c.CourseNo == id)
      .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
      .SingleOrDefaultAsync();
    }

    public async Task<List<CourseViewModel>> GetCoursesByCategoryAsync(string category)
    {
      var categories = await _context.Categories.ToListAsync();

      bool matchFound = false;

      foreach (var cat in categories)
      {
        if (category.ToLower() == cat.Name!.ToLower())
        {
          matchFound = true;
          break;
        }
      }
      if (matchFound == false)
      {
        throw new Exception("Kategori finns inte i systemet");
      }

      var courses = await _context.Courses.Include(m => m.Category)
      .Where(c => c.Category.Name!.ToLower() == category.ToLower())
        .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
        .ToListAsync();

      return courses;
    }

    public async Task<List<CourseViewModel>> ListAllCoursesAsync()
    {
      return await _context.Courses.
      ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).
      ToListAsync();
    }

    public async Task UpdateCourseAsync(PutCourseViewModel model, int id)
    {
      var category = await _context.Categories
      .Where(c => c.Name!.ToLower() == model.Category!.ToLower())
      .SingleOrDefaultAsync();

      if (category is null)
      {
        throw new Exception($"Tyvärr så finns inte ämnet {model.Category} i systemet");
      }

      // var teacher = await _context.Teachers
      // .Where(t => t.Id == model.TeacherId)
      // .SingleOrDefaultAsync();

      var teacher = await _context.Teachers
      .Where(t => t.Id == model.TeacherId)
      .Include(t => t.Competences)
      .ThenInclude(c => c.Competence)
      .SingleOrDefaultAsync();

      if (teacher is null)
      {
        throw new Exception($"Kunde inte hitta läraren med id {id} i vårt system");
      }

      // var course = await _context.Courses.FindAsync(id);
      var course = await _context.Courses
      .Where(c => c.Id == id).Include(c => c.Category)
      .SingleOrDefaultAsync();

      if (course is null)
      {
        throw new Exception($"Kunde ej hitta kurs med id: {id}");
      }

      var matchFound = false;

      foreach (var competence in teacher!.Competences)
      {
        if (competence!.Competence!.Name == course!.Category.Name)
        {
          matchFound = true;
          break;
        }
      }

      if (matchFound == false)
      {
        throw new Exception($"Läraren saknar denna kompetens som kursen kräver");
      }

      course.CourseNo = model.CourseNo;
      course.Name = model.Name;
      course.Duration = model.Duration;
      course.DurationUnit = model.DurationUnit;
      course.Category = category;
      // course.TeacherId = teacher.Id;
      course.TeacherId = model.TeacherId;
      course.Description = model.Description;
      course.SubCourses = model.SubCourses;

      _context.Courses.Update(course);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }



  }
}