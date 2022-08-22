using AutoMapper;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Courses_API.Controllers
{
  [ApiController]
  [Route("api/v1/courses")]
  public class CoursesController : ControllerBase
  {
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    public CoursesController(ICourseRepository courseRepository, IMapper mapper)
    {
      _mapper = mapper;
      _courseRepository = courseRepository;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<CourseViewModel>>> ListCourses()
    {
      var courseList = await _courseRepository.ListAllCoursesAsync();
      return Ok(courseList);
    }

    [HttpGet("bycategory/{category}")]
    public async Task<ActionResult<List<CourseViewModel>>> GetCoursesByCategory(string category)
    {
      try
      {
        var response = await _courseRepository.GetCoursesByCategoryAsync(category);
        if (response.Count < 1)
        {
          return NotFound($"Kan ej hitta kurs med ämnet: {category}");
        }
        else
        {
          await _courseRepository.SaveAllAsync();
        }

        return Ok(response);
      }
      catch (System.Exception ex)
      {
        return StatusCode(500, ex.Message);
      }

      // var response = await _courseRepository.GetCoursesByCategoryAsync(category);
      // if (response.Count < 1)
      // {
      //   return NotFound($"Kan ej hitta kurs med ämnet: {category}");
      // }
      // else
      // {
      //   await _courseRepository.SaveAllAsync();
      // }
      // return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseViewModel>> GetCourseWithId(int id)
    {
      var response = await _courseRepository.GetCourseAsync(id);
      if (response is null) return NotFound($"Kan ej hitta kurs med id: {id}");

      return Ok(response);
    }

    [HttpGet("edit/{id}")]
    public async Task<ActionResult<PutCourseViewModel>> GetCourseToUpdateWithId(int id)
    {
      var response = await _courseRepository.GetPutCourseAsync(id);
      if (response is null) return NotFound($"Kan ej hitta kurs med id: {id}");

      return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult> AddCourse(PostCourseViewModel model)
    {
      try
      {
        if (await _courseRepository.GetCourseByCourseNumberAsync(model.CourseNo) is not null)
        {
          var error = new ErrorViewModel
          {
            StatusCode = 400,
            StatusText = $"Kurs med kursnummer {model.CourseNo} finns redan i systemet"
          };

          return BadRequest(error);
        }

        await _courseRepository.AddCourseAsync(model);

        if (await _courseRepository.SaveAllAsync())
        {
          return StatusCode(201);
        }

        return StatusCode(500, "Det gick tyvärr inte att spara");
      }
      catch (Exception ex)
      {
        var error = new ErrorViewModel
        {
          StatusCode = 500,
          StatusText = ex.Message
        };
        return StatusCode(500, error);
      }

    }

    [HttpPut("edit/{id}")]
    public async Task<ActionResult> UpdateCourse(PutCourseViewModel model, int id)
    {
      try
      {
        await _courseRepository.UpdateCourseAsync(model, id);

        if (await _courseRepository.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, "Ett fel inträffade kursen skulle uppdateras");

      }


      catch (System.Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {

      try
      {
        await _courseRepository.DeleteCourseAsync(id);

        if (await _courseRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, "Något gick fel vid borttagning av studentkurs-registrering");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}