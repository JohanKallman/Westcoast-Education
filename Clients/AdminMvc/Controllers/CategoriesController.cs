using AdminMvc.Models;
using AdminMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminMvc.Controllers
{
  [Route("[controller]")]
  public class CategoriesController : Controller
  {
    private readonly IConfiguration _config;

    private readonly CategoryServiceModel _categoryService;
    public CategoriesController(IConfiguration config)
    {
      _config = config;
      _categoryService = new CategoryServiceModel(_config);
    }

    [HttpGet()]
    public async Task<IActionResult> Index()
    {
      try
      {
        var category = await _categoryService.ListAllCategories();

        return View("Index", category);
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

      var category = new CategoryPostViewModel();
      return View("Create", category);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CategoryPostViewModel category)
    {

      if (!ModelState.IsValid)
      {
        return View("Create", category);
      }

      if (await _categoryService.CreateCategory(category))
      {
        return View("Confirmation");
      }

      return View("Create", category);
    }

  }
}