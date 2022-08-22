using System.ComponentModel.DataAnnotations.Schema;
using Courses_API.Models;
using Courses_API.ViewModels.Categories;

namespace Courses_API
{
  public class PostTeacherCompetenceViewModel
  {
    public int CompetenceId { get; set; }
    public int TeacherId { get; set; }

  }
}