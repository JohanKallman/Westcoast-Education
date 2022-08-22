using AutoMapper;
using AutoMapper.QueryableExtensions;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels.Student;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
  public class StudentRepository : IStudentRepository
  {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public StudentRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task AddNewStudentAsync(RegisterStudentViewModel model)
    {
      var allStudents = await _context.Students.ToListAsync();
      foreach (var stu in allStudents)
      {
        if (stu.Email == model.Email)
        {
          throw new Exception($"Tyvärr så finns redan emailen: {model.Email} registrerad i systemet");
        }
      }

      var student = new Student
      {
        FirstName = model.FirstName,
        LastName = model.LastName,
        UserName = model.Email,
        Email = model.Email,
        Address = model.Address,
        PhoneNumber = model.PhoneNumber
      };
      await _context.Students.AddAsync(student);

    }

    public async Task AddCourseToStudent(PostStudentCourseViewModel model)
    {

      var allStudentCourses = await _context.StudentCourses.ToListAsync();

      var studentToCheck = await _context.Students.FindAsync(model.StudentId);

      var courseToCheck = await _context.Courses.FindAsync(model.CourseId);

      if (studentToCheck is null)
      {
        throw new Exception($"Studenten med id: {model.StudentId} finns inte i systemet");
      }

      if (courseToCheck is null)
      {
        throw new Exception($"Kursen med id: {model.CourseId} finns inte i systemet");
      }

      var studentRegistration = new StudentCourse();
      _mapper.Map<PostStudentCourseViewModel, StudentCourse>(model, studentRegistration);

      foreach (var reg in allStudentCourses)
      {
        if (reg.StudentId == studentRegistration.StudentId && reg.CourseId == studentRegistration.CourseId && reg.IsActive == true)
        {
          throw new Exception($"Studenten är redan registrerad på denna kurs");
        }
        else if (reg.StudentId == studentRegistration.StudentId && reg.CourseId == studentRegistration.CourseId && studentRegistration.IsActive == false)
        {
          studentRegistration.IsActive = true;
        }
      }

      await _context.StudentCourses.AddAsync(studentRegistration);

    }

    public async Task RemoveStudentFromCourseAsync(int id)
    {
      var response = await _context.StudentCourses.FindAsync(id);

      response!.EndDate = DateTime.Now;
      response.IsActive = false;
      _context.StudentCourses.Update(response);
    }

    public async Task DeleteStudentAsync(string id)
    {
      var response = await _context.Students.FindAsync(id);
      if (response is null)
      {
        throw new Exception($"Vi kunde inte hitta någon student med id {id}");
      }

      var studentCourses = await _context.StudentCourses.ToListAsync();
      foreach (var sc in studentCourses)
      {
        if (sc.StudentId == id)
        {
          throw new Exception($"Kan ej radera studenten med id: {id} Vänligen radera dennes historik");
        }
      }

      if (response is not null)
      {
        _context.Students.Remove(response);
      }
    }

    public async Task<List<StudentViewModel>> ListAllStudentsAsync()
    {
      return await _context.Students.Include(sc => sc.StudentCourses).ThenInclude(c => c.Course).ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<StudentViewModel?> ListStudentWithCoursesAsync(string id)
    {
      return await _context.Students.Where(s => s.Id == id)
      .Include(s => s.StudentCourses)
      .ThenInclude(sc => sc.Course)
      .ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

    }

    public async Task UpdateStudentAsync(string id, PutStudentViewModel model)
    {
      var student = await _context.Students.FindAsync(id);

      if (student is null) throw new Exception($"Kunde inte hitta studenten med id {id} i vårt system");
      student.Id = model.Id;
      student.FirstName = model.FirstName;
      student.LastName = model.LastName;
      student.Email = model.Email;
      student.Address = model.Address;
      student.PhoneNumber = model.PhoneNumber;
      _context.Students.Update(student);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

  }
}