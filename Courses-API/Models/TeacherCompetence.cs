using System.ComponentModel.DataAnnotations.Schema;

namespace Courses_API.Models
{
  public class TeacherCompetence
  {
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public int CompetenceId { get; set; }

    [ForeignKey("TeacherId")]
    public Teacher? Teacher { get; set; }

    [ForeignKey("CompetenceId")]
    public Category? Competence { get; set; }

  }
}