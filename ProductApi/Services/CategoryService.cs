using AutoMapper;
using ProductApi.DTOs;
using ProductApi.Models;
using ProductApi.Repositories;
using ProductApi.Services.Interface;

namespace ProductApi.Services;

public class CategoryService : ICategoryService
{
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public async Task<IEnumerable<CategoryDto>> GetCategories()
    {
        var categoriesEntity = await _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryDto>>(categoriesEntity);
    }

    public async Task<CategoryDto> GetCategoriesById(int id)
    {
        var categoriesEntity = await _categoryRepository.GetById(id);
        return _mapper.Map<CategoryDto>(categoriesEntity);
    }
    
    public async Task<IEnumerable<CategoryDto>> GetCategoriesProducts()
    {
        var categoriesEntity = await _categoryRepository.GetCategoriesProducts();
        return _mapper.Map<IEnumerable<CategoryDto>>(categoriesEntity);
    }

    public async Task CreateCategory(CategoryDto categoryDto)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.Create(categoryEntity);
        categoryDto.CategoryId = categoryEntity.CategoryId;;
    }

    public async Task UpdateCategory(CategoryDto categoryDto)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.Update(categoryEntity);
    }

    public async Task DeleteCategory(int id)
    {
        var categoryEntity = _categoryRepository.GetById(id);
        await _categoryRepository.Delete(categoryEntity.Id);
    }
}