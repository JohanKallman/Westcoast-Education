using System.ComponentModel.DataAnnotations;

namespace AdminMvc.ViewModels
{
  public class CategoryPostViewModel
  {
    [Required(ErrorMessage = "Ämnesnamn är obligatoriskt")]
    public string? Name { get; set; }

  }
}