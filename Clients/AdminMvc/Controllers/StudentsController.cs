using AdminMvc.Models;
using AdminMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminMvc.Controllers
{
  [Route("[controller]")]
  public class StudentsController : Controller
  {
    private readonly IConfiguration _config;
    private readonly StudentServiceModel _studentService;

    public StudentsController(IConfiguration config)
    {
      _config = config;
      _studentService = new StudentServiceModel(_config);
    }

    [HttpGet()]
    public async Task<IActionResult> Index()
    {
      try
      {
        var students = await _studentService.ListAllStudents();
        return View("Index", students);
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

      var student = new StudentPostViewModel();
      return View("Create", student);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(StudentPostViewModel student)
    {

      if (!ModelState.IsValid)
      {
        return View("Create", student);
      }

      if (await _studentService.CreateStudent(student))
      {
        return View("Confirmation");
      }

      return View("Create", student);
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(string id)
    {
      var student = await _studentService.FindStudentToUpdate(id);
      return View("Edit", student);
    }

    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(string id, StudentPutViewModel student)
    {

      if (!ModelState.IsValid)
      {
        return View("UpdateError", student);
      }

      if (await _studentService.UpdateStudent(id, student))
      {
        return View("UpdateConfirmed", student);
      }

      return View("Index");

    }

    [HttpGet("{id}/Details")]
    public async Task<IActionResult> Details(string id)
    {
      var student = await _studentService.FindStudentWithCourses(id);
      return View("Details", student);
    }

    [HttpGet("AddCourse")]
    public IActionResult AddCourse()
    {
      var studentCourse = new StudentPostCourseViewModel();
      return View("AddCourse", studentCourse);
    }

    [HttpPost("AddCourse")]
    public async Task<IActionResult> AddCourse(StudentPostCourseViewModel model)
    {

      if (!ModelState.IsValid)
      {
        return View("AddCourseError", model);
      }

      if (await _studentService.AddStudentCourse(model))
      {
        return View("AddCourseConfirmation");
      }

      return View("Create", model);
      //what??
    }

    [HttpGet("RemoveCourse/{id}")]
    public async Task<IActionResult> RemoveCourse(int id)
    {
      if (await _studentService.RemoveCourse(id))
      {
        return View("RemoveConfirmed");
      }

      return View("RemoveError");
    }

  }
}