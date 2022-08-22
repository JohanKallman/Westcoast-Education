using Courses_API.Interfaces;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
  [ApiController]
  [Route("api/v1/teachers")]
  public class TeacherController : ControllerBase
  {
    private readonly ITeacherRepository _teacherRepository;
    public TeacherController(ITeacherRepository teacherRepository)
    {
      _teacherRepository = teacherRepository;
    }

    [HttpPost()]
    public async Task<ActionResult> AddTeacher(PostTeacherViewModel model)
    {
      try
      {
        await _teacherRepository.AddNewTeacherAsync(model);
        if (await _teacherRepository.SaveAllAsync())
        {
          return StatusCode(201);
        }

        return StatusCode(500, "Det gick inte att spara ner läraren");
      }

      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }


    [HttpPost("competence")]
    public async Task<ActionResult> AddTeacherCompetence(PostTeacherCompetenceViewModel model)
    {
      try
      {
        await _teacherRepository.AddCompetenceToTeacher(model);
        if (await _teacherRepository.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, $"Något gick fel, det gick inte att ge läraren denna kompetens");
      }

      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<TeacherViewModel>>> ListTeachers()
    {
      var teacherList = await _teacherRepository.ListAllTeachersAsync();
      return Ok(teacherList);
    }


    [HttpGet("{id}")]
    public async Task<TeacherViewModel> GetTeacherWithId(int id)
    {
      var teacher = await _teacherRepository.ListATeacherWithCompetencesAsync(id);
      return (teacher!);
    }


    [HttpGet("{id}/competence")]
    public async Task<TeacherViewModel> ListTeacherWithCompetences(int id)
    {
      var teacher = await _teacherRepository.ListATeacherWithCompetencesAsync(id);
      return (teacher!);
    }


    [HttpGet("bycompetence/{competence}")]
    public async Task<ActionResult<List<TeacherViewModel>>> ListAllTeachersByCompetence(string competence)
    {

      try
      {
        var response = await _teacherRepository.GetTeachersByCompetenceAsync(competence);

        if (response.Count < 1)
        {
          return NotFound($"Kan ej hitta lärare med kompetensen: {competence}");
        }
        else
        {
          await _teacherRepository.SaveAllAsync();
        }

        return Ok(response);
      }
      catch (System.Exception ex)
      {
        return StatusCode(500, ex.Message);
      }

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTeacher(PutTeacherViewModel model, int id)
    {
      try
      {
        await _teacherRepository.UpdateTeacherAsync(id, model);

        if (await _teacherRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, $"Något gick fel, det gick inte att uppdatera läraren med id: {id}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeacher(int id)
    {
      try
      {
        await _teacherRepository.DeleteTeacherAsync(id);

        if (await _teacherRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, "Något gick fel vid borttagning av läraren");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

  }
}