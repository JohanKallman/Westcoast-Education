using AdminMvc.Models;
using AdminMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminMvc.Controllers
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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      try
      {
        var courses = await _courseService.ListAllCourses();

        return View("Index", courses);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }
    }
    [HttpGet("Create")]
    public IActionResult Create()
    {

      var course = new CoursePostViewModel();
      return View("Create", course);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CoursePostViewModel course)
    {

      if (!ModelState.IsValid)
      {
        return View("Create", course);
      }

      if (await _courseService.CreateCourse(course))
      {
        return View("Confirmation");
      }

      return View("Create", course);
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
      var course = await _courseService.FindCourseToUpdate(id);
      return View("Edit", course);
    }

    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(int id, CoursePutViewModel course)
    {

      if (!ModelState.IsValid)
      {
        return View("NoInput", course);
      }

      if (await _courseService.UpdateCourse(id, course))
      {
        return View("UpdateConfirmed", course);
      }
      return View("UpdateError", course);
    }

    [HttpGet("{id}/Details")]
    public async Task<IActionResult> Details(int id)
    {
      var course = await _courseService.FindCourseWithId(id);
      return View("Details", course);
    }

  }
}