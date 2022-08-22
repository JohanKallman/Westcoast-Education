using Microsoft.AspNetCore.Mvc;
using UserMvc.Models;
using UserMvc.ViewModels;

namespace UserMvc.Controllers
{

  [Route("[controller]")]
  public class AuthController : Controller
  {
    private readonly IConfiguration _config;
    private readonly AuthServiceModel _authService;

    public AuthController(IConfiguration config)
    {
      _config = config;
      _authService = new AuthServiceModel(_config);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
      var student = new RegisterStudentViewModel();
      return View("Create", student);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(RegisterStudentViewModel student)
    {

      if (!ModelState.IsValid)
      {
        return View("Create", student);
      }

      if (await _authService.CreateStudent(student))
      {
        return View("Confirmation");
      }

      return View("Create", student);
    }
  }
}