using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.Interfaces;
using Courses_API.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
  [ApiController]
  [Route("api/v1/categories")]
  public class CategoryController : ControllerBase
  {
    private readonly ICategoryRepository _categoryRepository;
    public CategoryController(ICategoryRepository categoryRepository)
    {
      _categoryRepository = categoryRepository;
    }

    [HttpGet("list")]
    public async Task<ActionResult> ListAllCategories()
    {
      var list = await _categoryRepository.ListCategoriesAsync();
      return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCategoryById(int id)
    {
      return Ok(await _categoryRepository.GetCategoryAsync(id));
    }

    [HttpGet("courses")]
    public async Task<ActionResult> ListCategoriesAndCourses()
    {
      return Ok(await _categoryRepository.ListCategoriesAndCoursesAsync());
    }

    [HttpGet("{id}/courses")]
    public async Task<ActionResult> ListCategoryCourses(int id)
    {
      var result = await _categoryRepository.ListCategoryCoursesAsync(id);
      if (result is null)
      {
        return NotFound($"Kunde ej hitta kurs med id {id}");
      }
      return Ok(result);
    }

    [HttpPost()]
    public async Task<ActionResult> AddNewCategory(PostCategoryViewModel model)
    {
      await _categoryRepository.AddCategoryAsync(model);
      if (await _categoryRepository.SaveAllAsync())
      {
        return StatusCode(201);
      }
      return StatusCode(500, "Det gick inte att spara ner ämnet");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategory(PutCategoryViewModel model, int id)
    {
      try
      {
        await _categoryRepository.UpdateCategoryAsync(id, model);

        if (await _categoryRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, $"Något gick fel, det gick inte att uppdatera ämneskategorin {model.Name}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
      try
      {
        await _categoryRepository.DeleteCategoryAsync(id);


        if (await _categoryRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, $"Det gick inte att ta bort ämnet med id {id}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

  }
}