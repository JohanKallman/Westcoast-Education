using Courses_API.ViewModels.Categories;

namespace Courses_API.Interfaces
{
  public interface ICategoryRepository
  {
    public Task AddCategoryAsync(PostCategoryViewModel model);
    public Task<List<CategoryViewModel>> ListCategoriesAsync();
    public Task<CategoryViewModel> GetCategoryAsync(int id);
    public Task<List<CategoryWithCoursesViewModel>> ListCategoriesAndCoursesAsync();
    public Task<CategoryWithCoursesViewModel?> ListCategoryCoursesAsync(int id);

    public Task UpdateCategoryAsync(int id, PutCategoryViewModel model);
    public Task DeleteCategoryAsync(int id);
    public Task<bool> SaveAllAsync();


  }
}