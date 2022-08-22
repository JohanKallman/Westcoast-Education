using System.ComponentModel;

namespace AdminMvc.ViewModels
{
  public class TeacherPostCompetenceViewModel
  {
    [DisplayName("KategoriID")]
    public int CompetenceId { get; set; }
    [DisplayName("LärarID")]
    public int TeacherId { get; set; }

  }
}