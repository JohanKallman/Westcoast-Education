using AutoMapper;
using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Categories;
using Courses_API.ViewModels.Course;
using Courses_API.ViewModels.Student;
using Courses_API.ViewModels.Teacher;
using Courses_API.ViewModels.TeacherCompetence;

namespace Courses_API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {

      CreateMap<PostCourseViewModel, Course>()
      .ForMember(dest => dest.Category, options => options.
      Ignore());

      CreateMap<Course, CourseViewModel>()
      .ForMember(dest => dest.CourseId, options => options.
      MapFrom(src => src.Id)).
      ForMember(dest => dest.Category, options => options.
      MapFrom(src => src.Category.Name)).
      ForMember(dest => dest.Teacher, options => options.
      MapFrom(src => string.Concat(src.Teacher!.FirstName, " ", src.Teacher.LastName)));
      // ForMember(dest => dest.Duration, options => options.
      // MapFrom(src => string.Concat(src.Duration, " ", src.DurationUnit)));

      CreateMap<PostCategoryViewModel, Category>();
      CreateMap<Category, CategoryViewModel>()
      .ForMember(dest => dest.CategoryId, options => options.
      MapFrom(src => src.Id));

      CreateMap<RegisterStudentViewModel, Student>();
      CreateMap<Student, StudentViewModel>();

      CreateMap<PostStudentCourseViewModel, StudentCourse>();
      CreateMap<StudentCourse, StudentCourseViewModel>();

      CreateMap<PostTeacherCompetenceViewModel, TeacherCompetence>();
      CreateMap<TeacherCompetence, TeacherCompetenceViewModel>();

      CreateMap<PostTeacherViewModel, Teacher>();
      CreateMap<Teacher, TeacherViewModel>();


      CreateMap<PutCourseViewModel, Course>()
      // .ForMember(dest => dest.TeacherId, options => options.
      // MapFrom(src => src.Id))
      // .ForMember(dest => dest.Category.Name, options => options.
      // MapFrom(src => src.Category))
      ;

      CreateMap<Course, PutCourseViewModel>()
      // .ForMember(dest => dest.Id, options => options.
      // MapFrom(src => src.TeacherId))
      //  .ForMember(dest => dest.Category, options => options.
      //   MapFrom(src => src.Category.Name))
      ;

    }
  }
}