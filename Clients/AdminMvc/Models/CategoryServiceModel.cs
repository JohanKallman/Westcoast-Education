using System.Text.Json;
using AdminMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminMvc.Models
{
  public class CategoryServiceModel
  {
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _options;
    private readonly IConfiguration _config;

    public CategoryServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}/categories";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }
    public async Task<List<CategoryViewModel>> ListAllCategories()
    {
      var url = $"{_baseUrl}/list";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var categories = await response.Content.ReadFromJsonAsync<List<CategoryViewModel>>();

      return categories ?? new List<CategoryViewModel>();
    }

    public async Task<bool> CreateCategory(CategoryPostViewModel category)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}";

      var response = await http.PostAsJsonAsync(url, category);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<CategoryViewModel> FindCategory(int id)
    {
      var url = $"{_baseUrl}/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inge vidare");
      }

      var category = await response.Content.ReadFromJsonAsync<CategoryViewModel>();

      return category!;
    }

  }
}