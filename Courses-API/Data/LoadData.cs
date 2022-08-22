using System.Text.Json;
using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Teacher;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Data
{
  public class LoadData
  {

    public static async Task LoadCategories(CourseContext context)
    {
      if (await context.Categories.AnyAsync()) return;

      var categoryData = await File.ReadAllTextAsync("Data/category.json");
      var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

      await context.AddRangeAsync(categories!);
      await context.SaveChangesAsync();
    }

    public static async Task LoadTeachers(CourseContext context)
    {
      if (await context.Teachers.AnyAsync()) return;

      var teacherWCData = await File.ReadAllTextAsync("Data/teachers.json");
      var teachersWithoutCompetences = JsonSerializer.Deserialize<List<Teacher>>(teacherWCData);

      await context.AddRangeAsync(teachersWithoutCompetences!);
      await context.SaveChangesAsync();

    }

    public static async Task LoadTeacherCompetences(CourseContext context)
    {

      if (await context.TeacherCompetences.AnyAsync()) return;

      var teacherCompetenceData = await File.ReadAllTextAsync("Data/teachercompetences.json");
      var teacherCompetences = JsonSerializer.Deserialize<List<PostTeacherCompetenceViewModel>>(teacherCompetenceData);

      if (teacherCompetences is null) return;

      foreach (var tc in teacherCompetences)
      {
        var competence = await context.Categories.SingleOrDefaultAsync(cat => cat.Id == tc.CompetenceId);
        var teacher = await context.Teachers.SingleOrDefaultAsync(t => t.Id == tc.TeacherId);

        if (teacher is not null && competence is not null)
        {
          var newTeacherCompetence = new TeacherCompetence
          {
            CompetenceId = competence.Id,
            TeacherId = teacher.Id
          };
          context.TeacherCompetences.Add(newTeacherCompetence);
        }
      }

      await context.SaveChangesAsync();

    }
    public static async Task LoadCourses(CourseContext context)
    {
      if (await context.Courses.AnyAsync()) return;

      var courseData = await File.ReadAllTextAsync("Data/courses.json");
      var courses = JsonSerializer.Deserialize<List<PostCourseViewModel>>(courseData);

      if (courses is null) return;

      foreach (var course in courses)
      {
        var category = await context.Categories.SingleOrDefaultAsync(cat => cat.Name.ToLower() == course.Category!.ToLower());
        var teacher = await context.Teachers.Where(t => t.Id == course.TeacherId).SingleOrDefaultAsync();

        if (category is not null && teacher is not null)
        {
          var newCourse = new Course
          {
            CourseNo = course.CourseNo,
            Name = course.Name,
            Duration = course.Duration,
            DurationUnit = course.DurationUnit,
            Category = category,
            TeacherId = teacher.Id,
            Description = course.Description,
            SubCourses = course.SubCourses,
          };

          context.Courses.Add(newCourse);
        }
      }
      await context.SaveChangesAsync();
    }


  }
}