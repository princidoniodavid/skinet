using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository: IProductRepository
{
    private readonly StoreContext _storeContext;

    public ProductRepository(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _storeContext.Products
            .Include(x=>x.ProductType)
            .Include(x=>x.ProductBrand)
            .FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return (await _storeContext.Products
            .Include(x=>x.ProductType)
            .Include(x=>x.ProductBrand)
            .ToListAsync())!;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _storeContext.ProductBrands.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _storeContext.ProductsTypes.ToListAsync();
    }
}