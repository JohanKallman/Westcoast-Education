using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
  [ApiController]
  [Route("api/v1/teachercompetence")]
  public class TeacherCompetenceController : ControllerBase
  {
    private readonly ITeacherCompetenceRepository _teacherCompetenceRepository;
    public TeacherCompetenceController(ITeacherCompetenceRepository teacherCompetenceRepository)
    {
      _teacherCompetenceRepository = teacherCompetenceRepository;
    }

    [HttpPost()]
    public async Task<ActionResult> AddNewTeacherCompetence(PostTeacherCompetenceViewModel teacherCompetenceViewModel)
    {
      try
      {
        await _teacherCompetenceRepository.AddTeacherCompetenceAsync(teacherCompetenceViewModel);

        if (await _teacherCompetenceRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, "Det gick inte att registrera studenten på denna kurs");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeacherCompetence(int id)
    {

      try
      {
        await _teacherCompetenceRepository.DeleteTeacherCompetenceAsync(id);

        if (await _teacherCompetenceRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, "Något gick fel vid borttagning av kompetensen");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

  }
}