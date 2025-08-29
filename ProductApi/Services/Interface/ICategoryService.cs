using ProductApi.DTOs;
using ProductApi.Models;

namespace ProductApi.Services.Interface;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategories();
    Task<IEnumerable<CategoryDto>> GetCategoriesProducts();
    Task<CategoryDto> GetCategoriesById(int id);
    Task CreateCategory(CategoryDto category);
    Task UpdateCategory(CategoryDto category);
    Task DeleteCategory(int id);   
}