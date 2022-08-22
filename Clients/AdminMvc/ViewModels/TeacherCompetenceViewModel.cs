using System.ComponentModel.DataAnnotations.Schema;

namespace AdminMvc.ViewModels
{
  public class TeacherCompetenceViewModel
  {
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public int CompetenceId { get; set; }

    [ForeignKey("TeacherId")]
    public TeacherViewModel Teacher { get; set; } = new TeacherViewModel();

    [ForeignKey("CompetenceId")]
    public CategoryViewModel Competence { get; set; } = new CategoryViewModel();

  }
}