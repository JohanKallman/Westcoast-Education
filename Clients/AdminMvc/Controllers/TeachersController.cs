using AdminMvc.Models;
using AdminMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminMvc.Controllers
{
  [Route("[controller]")]
  public class TeachersController : Controller
  {

    private readonly IConfiguration _config;
    private readonly TeacherServiceModel _teacherService;

    public TeachersController(IConfiguration config)
    {
      _config = config;
      _teacherService = new TeacherServiceModel(_config);
    }

    [HttpGet()]
    public async Task<IActionResult> Index(string category)
    {
      try
      {
        var teachers = await _teacherService.ListAllTeachers();

        if (!string.IsNullOrEmpty(category))
        {
          teachers = await _teacherService.FilterTeachers(category);
        }

        return View("Index", teachers);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("FilterError");
      }
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
      var teacher = new TeacherPostViewModel();
      return View("Create", teacher);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(TeacherPostViewModel teacher)
    {

      if (!ModelState.IsValid)
      {
        return View("Create", teacher);
      }

      if (await _teacherService.CreateTeacher(teacher))
      {
        return View("Confirmation");
      }

      return View("Create", teacher);
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
      var teacher = await _teacherService.FindTeacherToUpdate(id);
      return View("Edit", teacher);
    }

    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(int id, TeacherPutViewModel teacher)
    {
      // if (ModelState.IsValid)
      // {
      //   await _teacherService.UpdateTeacher(id, teacher);
      //   return View("UpdateConfirmed", teacher);
      // }
      // return View("UpdateError", teacher);

      if (!ModelState.IsValid)
      {
        return View("UpdateError", teacher);
      }

      if (await _teacherService.UpdateTeacher(id, teacher))
      {
        return View("UpdateConfirmed", teacher);
      }

      return View("Index");
    }

    [HttpGet("{id}/Details")]
    public async Task<IActionResult> Details(int id)
    {
      var teacher = await _teacherService.FindTeacherWithCategories(id);
      return View("Details", teacher);
    }

    [HttpGet("AddCompetence")]
    public IActionResult AddCompetence()
    {
      var teacherCompetence = new TeacherPostCompetenceViewModel();
      return View("AddCompetence", teacherCompetence);
    }

    [HttpPost("AddCompetence")]
    public async Task<IActionResult> AddCompetence(TeacherPostCompetenceViewModel model)
    {
      if (ModelState.IsValid)
      {
        await _teacherService.AddTeacherCompetence(model);
        return View("AddCompConfirmation");
      }

      return View("AddCompError");

      // if (!ModelState.IsValid)
      // {
      //   return View("AddCompError", model);
      // }

      // if (await _teacherService.AddTeacherCompetence(model))
      // {
      //   return View("AddCompConfirmation");
      // }

      // return View("Create", model);
    }

  }
}