using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<ProductBrand> _brandRepository;
    private readonly IGenericRepository<ProductType> _typeRepository;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> brandRepository, IGenericRepository<ProductType> typeRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _typeRepository = typeRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProducts()
    {
        var products =
            await _productRepository.ListAsync(new ProductsWithTypesAndBrandsSpecification());
        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _productRepository.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpecification(id));
        if (product == null) return NotFound(new ApiResponse(404));
        return Ok(_mapper.Map<Product, ProductDto>(product));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _brandRepository.ListAllAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await _typeRepository.ListAllAsync());
    }
}