using ProductApi.DTOs;
using ProductApi.Models;

namespace ProductApi.Services.Interface;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<ProductDto> GetProductById(int id);
    Task AddProduct(ProductDto product);
    Task UpdateProduct(ProductDto product);
    Task DeleteProduct(int id);
}