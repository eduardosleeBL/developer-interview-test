using Microsoft.EntityFrameworkCore;
using Smartwyre.DeveloperTest.DataStore.Data;

namespace Smartwyre.DeveloperTest.DataStore.Products;

public class ProductDataStore : IProductDataStore
{
    private readonly SmartwyreDbContext _smartwyreDbContext;

    public ProductDataStore(SmartwyreDbContext smartwyreDbContext)
    {
        _smartwyreDbContext = smartwyreDbContext;
    }

    public async Task<Product?> GetProductAsync(string productIdentifier)
    {
        return await _smartwyreDbContext.Products
                .FirstOrDefaultAsync(product => product.Identifier == productIdentifier);
    }
}