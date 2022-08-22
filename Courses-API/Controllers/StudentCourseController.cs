using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
  [ApiController]
  [Route("api/v1/studentcourseadmin")]
  public class StudentCourseController : ControllerBase
  {
    private readonly IStudentCourseRepository _studentCourseRepository;
    public StudentCourseController(IStudentCourseRepository studentCourseRepository)
    {
      _studentCourseRepository = studentCourseRepository;
    }

    [HttpPost()]
    public async Task<ActionResult> AddNewStudentCourse(PostStudentCourseViewModel studentCourseViewModel)
    {
      try
      {
        await _studentCourseRepository.AddStudentCourseAsync(studentCourseViewModel);

        if (await _studentCourseRepository.SaveAllAsync())
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

    [HttpGet()]
    public async Task<ActionResult<List<StudentCourseViewModel>>> ListAllStudentRegistrations()
    {
      var studentCoursesList = await _studentCourseRepository.ListAllStudentRegistrationsAsync();
      return Ok(studentCoursesList);
    }

    [HttpDelete("{id}/delete")]
    public async Task<ActionResult> RegistrationRecord(int id)
    {

      try
      {
        await _studentCourseRepository.DeleteStudentRegistrationRecordAsync(id);

        if (await _studentCourseRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, "Något gick fel vid borttagning av studentkursen");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudentCourseRegistration(int id)
    {

      try
      {
        await _studentCourseRepository.RemoveStudentFromCourseAsync(id);

        if (await _studentCourseRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, "Något gick fel vid borttagning av studentkursen");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }



  }
}