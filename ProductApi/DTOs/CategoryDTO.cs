using System.ComponentModel.DataAnnotations;
using ProductApi.Models;

namespace ProductApi.DTOs;

public class CategoryDto
{
    public int CategoryId { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3)]
    [StringLength(100)]
    public string? Name { get; set; }
    public ICollection<Product>? Products { get; set; }
}