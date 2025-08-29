using Microsoft.AspNetCore.Mvc;
using ProductApi.DTOs;
using ProductApi.Services.Interface;

namespace ProductApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly  ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var categoriesDto = await _categoryService.GetCategories();
        if (categoriesDto is null)
        {
            return NotFound("Categories not found"); 
        }
        return Ok(categoriesDto);
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesWithProducts()
    {
        var categoriesDto = await _categoryService.GetCategoriesProducts();
        if (categoriesDto is null)
        {
            return NotFound("Categories not found");
        }
        return Ok(categoriesDto);
    }
    
    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var categoryDto = await _categoryService.GetCategoriesById(id);
        if (categoryDto == null)
        {
            return NotFound($"Category with ID {id} not found");
        }
        return Ok(categoryDto);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CategoryDto categoryDto)
    {
        if (categoryDto == null)
        {
            return BadRequest("Category data is null");
        }
        
        await _categoryService.CreateCategory(categoryDto);
        
        return new CreatedAtRouteResult("GetCategory", 
            new { id = categoryDto.CategoryId }, categoryDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] CategoryDto categoryDto)
    {
        if (id != categoryDto.CategoryId)
        {
            return BadRequest("Category ID mismatch");
        }

        if (categoryDto == null)
        {
            return BadRequest("Category data is null");
        }

        await _categoryService.UpdateCategory(categoryDto);
        return Ok(categoryDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Delete(int id)
    {
        var categoryDto = await _categoryService.GetCategoriesById(id);
        if (categoryDto == null)
        {
            return NotFound("Category not found");
        }
        await _categoryService.DeleteCategory(id);
        return Ok();
    }
}