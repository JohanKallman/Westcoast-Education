using Courses_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Data
{

  public class CourseContext : IdentityDbContext<Student>
  {
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();
    public DbSet<TeacherCompetence> TeacherCompetences => Set<TeacherCompetence>();

    public CourseContext(DbContextOptions options) : base(options)
    {
    }


  }
}