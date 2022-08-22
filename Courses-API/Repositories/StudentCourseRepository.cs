using AutoMapper;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
  public class StudentCourseRepository : IStudentCourseRepository
  {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public StudentCourseRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;

    }
    public async Task AddStudentCourseAsync(PostStudentCourseViewModel studentCourse)
    {
      var allRegistrations = await _context.StudentCourses.ToListAsync();

      var courseToCheck = await _context.Courses.FindAsync(studentCourse.CourseId);
      var studentToCheck = await _context.Students.FindAsync(studentCourse.StudentId);

      if (courseToCheck is null)
      {
        throw new Exception($"Kursen med id: {studentCourse.CourseId} finns inte i systemet");
      }

      if (studentToCheck is null)
      {
        throw new Exception($"Studenten med id: {studentCourse.StudentId} finns inte i systemet");
      }

      var studentRegistration = new StudentCourse();
      _mapper.Map<PostStudentCourseViewModel, StudentCourse>(studentCourse, studentRegistration);

      foreach (var reg in allRegistrations)
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

    public async Task DeleteStudentRegistrationRecordAsync(int id)
    {
      var response = await _context.StudentCourses.FindAsync(id);
      if (response is null)
      {
        throw new Exception($"Vi kunde inte hitta någon studentkurs-registrering med id {id}");
      }
      if (response is not null)
      {
        _context.StudentCourses.Remove(response);
      }
    }

    public async Task RemoveStudentFromCourseAsync(int id)
    {
      var response = await _context.StudentCourses.FindAsync(id);
      if (response is null)
      {
        throw new Exception($"Vi kunde inte hitta någon studentkurs-registrering med id {id}");
      }

      response.EndDate = DateTime.Today;
      response.IsActive = false;

    }

    public async Task<List<StudentCourseViewModel>> ListAllStudentRegistrationsAsync()
    {

      var studentRegistrations = await _context.StudentCourses
      .Include(c => c.Course)
      .ThenInclude(cat => cat!.Category)
      .Include(c => c.Course)
      .ThenInclude(t => t!.Teacher)
      .Include(s => s.Student)
      .ToListAsync();

      var studentCoursesList = new List<StudentCourseViewModel>();

      foreach (var registration in studentRegistrations)
      {
        var studentRegistrationsAsViewModel = new StudentCourseViewModel
        {
          Id = registration.Id,
          CourseId = registration.CourseId,
          StudentId = registration.StudentId,
          Course = new CourseViewModel
          {
            CourseId = registration.Course!.Id,
            CourseNo = registration.Course.CourseNo,
            Name = registration.Course.Name,
            Duration = string.Concat(registration.Course.Duration, " ", registration.Course.DurationUnit),
            Category = registration.Course.Category.Name,
            Teacher = string.Concat(registration.Course.Teacher!.FirstName, " ", registration.Course.Teacher.LastName),
            Description = registration.Course.Description,
            SubCourses = registration.Course.SubCourses
          },
          Student = new StudentViewModel
          {
            Id = registration.Student!.Id,
            FirstName = registration.Student!.FirstName,
            LastName = registration.Student.LastName,
            Email = registration.Student.Email,
            //UserName = registration.Student.UserName,
            Address = registration.Student.Address,
            PhoneNumber = registration.Student.PhoneNumber
          },
          StartDate = registration.StartDate,
          EndDate = registration.EndDate,
          IsActive = registration.IsActive

        };
        studentCoursesList.Add(studentRegistrationsAsViewModel);
      }

      return studentCoursesList;
    }
    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}