using System.Text.Json;
using AdminMvc.ViewModels;

namespace AdminMvc.Models
{
  public class TeacherServiceModel
  {
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _options;
    private readonly IConfiguration _config;

    public TeacherServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}/teachers";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }

    public async Task<List<TeacherViewModel>> ListAllTeachers()
    {
      var url = $"{_baseUrl}/list";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var teachers = await response.Content.ReadFromJsonAsync<List<TeacherViewModel>>();

      return teachers ?? new List<TeacherViewModel>();
    }

    public async Task<bool> CreateTeacher(TeacherPostViewModel teacher)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}";

      var response = await http.PostAsJsonAsync(url, teacher);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<TeacherPutViewModel> FindTeacherToUpdate(int id)
    {
      var url = $"{_baseUrl}/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var teacher = await response.Content.ReadFromJsonAsync<TeacherPutViewModel>();

      return teacher!;
    }

    public async Task<bool> UpdateTeacher(int id, TeacherPutViewModel teacher)
    {
      var url = $"{_baseUrl}/{teacher.Id}";

      using var http = new HttpClient();
      var response = await http.PutAsJsonAsync(url, teacher);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<TeacherViewModel> FindTeacherWithCategories(int id)
    {
      var url = $"{_baseUrl}/{id}/competence";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var teacher = await response.Content.ReadFromJsonAsync<TeacherViewModel>();

      return teacher!;
    }

    public async Task<bool> AddTeacherCompetence(TeacherPostCompetenceViewModel model)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}/competence";

      var response = await http.PostAsJsonAsync(url, model);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<List<TeacherViewModel>> FilterTeachers(string category)
    {

      var url = $"{_baseUrl}/bycompetence/{category}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inte att hitta lärare med dessa kompetenser");
      }

      var teachers = await response.Content.ReadFromJsonAsync<List<TeacherViewModel>>();

      return teachers ?? new List<TeacherViewModel>();
    }

  }
}