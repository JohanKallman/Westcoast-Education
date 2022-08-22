using Microsoft.AspNetCore.Mvc;
using UserMvc.Models;

namespace UserMvc.Controllers
{
  [Route("[controller]")]
  public class CoursesController : Controller
  {
    private readonly IConfiguration _config;
    private readonly CourseServiceModel _courseService;

    public CoursesController(IConfiguration config)
    {
      _config = config;
      _courseService = new CourseServiceModel(_config);
    }

    [HttpGet()]
    public async Task<IActionResult> Index(string category)
    {
      try
      {
        var courses = await _courseService.ListAllCourses();

        if (!string.IsNullOrEmpty(category))
        {
          courses = await _courseService.FilterCourses(category);
        }

        return View("Index", courses);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("FilterError");
      }
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
      try
      {
        var course = await _courseService.FindCourse(id);
        return View("Details", course);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }
    }

  }
}