using AutoMapper;
using ProductApi.DTOs;
using ProductApi.Models;
using ProductApi.Repositories;
using ProductApi.Services.Interface;

namespace ProductApi.Services;

public class ProductService : IProductService
{
    
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    
    public ProductService(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var productsEntity = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDto>>(productsEntity);
    }

    public async Task<ProductDto> GetProductById(int id)
    {
        var productEntity = await _productRepository.GetById(id);
        return _mapper.Map<ProductDto>(productEntity);
    }

    public async Task AddProduct(ProductDto productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _productRepository.Create(productEntity);
    }

    public async Task UpdateProduct(ProductDto productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _productRepository.Update(productEntity);
    }

    public async Task DeleteProduct(int id)
    {
        var productEntity = await _productRepository.GetById(id);
        await _productRepository.Delete(productEntity.Id);
    }
}