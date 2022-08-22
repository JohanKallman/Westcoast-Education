using System.Text.Json;
using AdminMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminMvc.Models
{
  public class StudentServiceModel
  {
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _options;
    private readonly IConfiguration _config;

    public StudentServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}/students";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }

    public async Task<List<StudentViewModel>> ListAllStudents()
    {
      var url = $"{_baseUrl}/list";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var students = await response.Content.ReadFromJsonAsync<List<StudentViewModel>>();

      return students ?? new List<StudentViewModel>();
    }

    public async Task<bool> CreateStudent(StudentPostViewModel student)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}";

      var response = await http.PostAsJsonAsync(url, student);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<StudentPutViewModel> FindStudentToUpdate(string id)
    {
      var url = $"{_baseUrl}/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var student = await response.Content.ReadFromJsonAsync<StudentPutViewModel>();

      return student!;
    }

    public async Task<StudentViewModel> FindStudentWithCourses(string id)
    {
      var url = $"{_baseUrl}/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var student = await response.Content.ReadFromJsonAsync<StudentViewModel>();

      return student!;
    }

    public async Task<bool> AddStudentCourse(StudentPostCourseViewModel model)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}/courses";

      var response = await http.PostAsJsonAsync(url, model);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<bool> UpdateStudent(string id, StudentPutViewModel student)
    {
      var url = $"{_baseUrl}/{student.Id}";

      using var http = new HttpClient();
      var response = await http.PutAsJsonAsync(url, student);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<bool> RemoveCourse(int id)
    {
      var url = $"{_baseUrl}/{id}";
      using var http = new HttpClient();

      var response = await http.PostAsJsonAsync(url, id);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;

    }
  }
}