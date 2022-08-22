using AutoMapper;
using AutoMapper.QueryableExtensions;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
  public class CategoryRepository : ICategoryRepository
  {
    private readonly IMapper _mapper;
    private readonly CourseContext _context;
    public CategoryRepository(CourseContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task AddCategoryAsync(PostCategoryViewModel model)
    {
      var category = _mapper.Map<Category>(model);
      await _context.Categories.AddAsync(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
      var result = await _context.Categories.FindAsync(id);

      if (result is null) throw new Exception($"Kunde inte hitta tillverkare med id {id}");

      _context.Categories.Remove(result);
    }

    public async Task<CategoryViewModel> GetCategoryAsync(int id)
    {
      return _mapper.Map<CategoryViewModel>(await _context.Categories.FindAsync(id));
    }

    public async Task<List<CategoryWithCoursesViewModel>> ListCategoriesAndCoursesAsync()
    {  
      return await _context.Categories.Include(c => c.Courses)
      .Select(m => new CategoryWithCoursesViewModel
      {
        CategoryId = m.Id,
        Name = m.Name,
        Courses = m.Courses.Select(cvm => new CourseViewModel
        {
          CourseId = cvm.Id,
          CourseNo = cvm.CourseNo,
          Name = cvm.Name,
          Duration = string.Concat(cvm.Duration, " ", cvm.DurationUnit),
          Category = cvm.Category.Name,
          Description = cvm.Description,
          SubCourses = cvm.SubCourses
        }).ToList()
      })
      .ToListAsync();
    }

    public async Task<List<CategoryViewModel>> ListCategoriesAsync()
    {
      return await _context.Categories.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<CategoryWithCoursesViewModel?> ListCategoryCoursesAsync(int id)
    {
      return await _context.Categories.Where(c => c.Id == id).Include(c => c.Courses)
  .Select(m => new CategoryWithCoursesViewModel
  {
    CategoryId = m.Id,
    Name = m.Name,
    Courses = m.Courses.Select(cvm => new CourseViewModel
    {
      CourseId = cvm.Id,
      CourseNo = cvm.CourseNo,
      Name = cvm.Name,
      Duration = string.Concat(cvm.Duration, " ", cvm.DurationUnit),
      Category = cvm.Category.Name,
      Description = cvm.Description,
      SubCourses = cvm.SubCourses
    }).ToList()
  })
  .SingleOrDefaultAsync();
    }
    public async Task UpdateCategoryAsync(int id, PutCategoryViewModel model)
    {
      var category = await _context.Categories.FindAsync(id);

      if (category is null) throw new Exception($"Kunde inte hitta ämnet med namnet {model.Name} i vårt system");

      category.Name = model.Name;

      _context.Categories.Update(category);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }


  }
}