using Courses_API.Models;
using Courses_API.ViewModels.Student;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
  [ApiController]
  [Route("api/v1/auth")]
  public class AuthController : ControllerBase
  {
    private readonly UserManager<Student> _userManager;
    private readonly SignInManager<Student> _signInManager;
    public AuthController(UserManager<Student> userManager, SignInManager<Student> signInManager)
    {
      _signInManager = signInManager;
      _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<ActionResult<StudentViewModel>> RegisterStudent(RegisterStudentViewModel model)
    {
      var user = new Student
      {
        UserName = model.Email!.ToLower(),
        Email = model.Email!.ToLower(),
        FirstName = model.FirstName!.ToLower(),
        LastName = model.LastName!.ToLower(),
        PhoneNumber = model.PhoneNumber!.ToLower(),
        Address = model.Address!.ToLower()
      };
      var result = await _userManager.CreateAsync(user);
      if (result.Succeeded)
      {
        var userData = new StudentViewModel
        {
          //UserName = user.UserName,
          FirstName = user.FirstName,
          LastName = user.LastName,
          Email = user.Email,
          PhoneNumber = user.PhoneNumber,
          Address = user.Address
        };

        return StatusCode(201, userData);
      }
      else
      {
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError("User registration", error.Description);
        }
        return StatusCode(500, ModelState);
      }

    }

  }
}