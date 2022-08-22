using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
  [ApiController]
  [Route("api/v1/students")]
  public class StudentController : ControllerBase
  {
    private readonly IStudentRepository _studentRepository;
    public StudentController(IStudentRepository studentRepository)
    {
      _studentRepository = studentRepository;
    }

    [HttpPost()]
    public async Task<ActionResult> AddNewStudent(RegisterStudentViewModel model)
    {
      try
      {
        await _studentRepository.AddNewStudentAsync(model);

        if (await _studentRepository.SaveAllAsync())
        {
          return StatusCode(201);
        }

        return StatusCode(500, "Det gick inte att spara ner studenten");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpPost("courses")]
    public async Task<ActionResult> AddStudentCourse(PostStudentCourseViewModel model)
    {
      try
      {
        await _studentRepository.AddCourseToStudent(model);
        if (await _studentRepository.SaveAllAsync())
        {
          // return NoContent();
          return StatusCode(201);
        }
        return StatusCode(500, $"Något gick fel, det gick inte att ge studenten denna kurs");
      }

      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<StudentViewModel>>> ListStudents()
    {
      var studentList = await _studentRepository.ListAllStudentsAsync();
      return Ok(studentList);
    }

    [HttpGet("{id}")]
    public async Task<StudentViewModel> ListStudentCoursesById(string id)
    {
      var student = await _studentRepository.ListStudentWithCoursesAsync(id);
      return (student!);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> RemoveCourse(int id)
    {
      await _studentRepository.RemoveStudentFromCourseAsync(id);
      await _studentRepository.SaveAllAsync();
      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStudent(PutStudentViewModel model, string id)
    {
      try
      {
        await _studentRepository.UpdateStudentAsync(id, model);

        if (await _studentRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, $"Något gick fel, det gick inte att uppdatera studenten med id: {id}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudent(string id)
    {

      try
      {
        await _studentRepository.DeleteStudentAsync(id);

        if (await _studentRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, "Något gick fel vid borttagning av studenten");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

  }
}