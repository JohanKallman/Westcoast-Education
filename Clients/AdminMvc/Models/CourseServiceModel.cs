using System.Text.Json;
using AdminMvc.ViewModels;

namespace AdminMvc.Models
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

    public async Task<bool> CreateCourse(CoursePostViewModel course)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}";

      var response = await http.PostAsJsonAsync(url, course);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<CoursePutViewModel> FindCourseToUpdate(int id)
    {
      var url = $"{_baseUrl}/edit/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var course = await response.Content.ReadFromJsonAsync<CoursePutViewModel>();

      return course!;
    }

    public async Task<bool> UpdateCourse(int id, CoursePutViewModel course)
    {
      var url = $"{_baseUrl}/edit/{course.Id}";

      using var http = new HttpClient();
      var response = await http.PutAsJsonAsync(url, course);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();

        return false;
      }
      return true;
    }

    public async Task<CourseViewModel> FindCourseWithId(int id)
    {
      var url = $"{_baseUrl}/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var course = await response.Content.ReadFromJsonAsync<CourseViewModel>();

      return course!;
    }

  }
}