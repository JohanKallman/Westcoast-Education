using System.Text.Json;
using UserMvc.ViewModels;

namespace UserMvc.Models
{
  public class CourseServiceModel
  {
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _options;
    private readonly IConfiguration _config;

    public CourseServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}/courses";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }
    public async Task<List<CourseViewModel>> ListAllCourses()
    {
      var url = $"{_baseUrl}/list";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var courses = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();

      return courses ?? new List<CourseViewModel>();
    }

    public async Task<CourseViewModel> FindCourse(int id)
    {
      var baseUrl = _config.GetValue<string>("baseUrl");
      var url = $"{baseUrl}/courses/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        Console.WriteLine("Det gick inte att hitta kursen");
      }

      var course = await response.Content.ReadFromJsonAsync<CourseViewModel>();

      return course ?? new CourseViewModel();
    }

    public async Task<List<CourseViewModel>> FilterCourses(string category)
    {

      var url = $"{_baseUrl}/bycategory/{category}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inte att hitta kursen");
      }

      var courses = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();

      return courses ?? new List<CourseViewModel>();
    }


  }
}