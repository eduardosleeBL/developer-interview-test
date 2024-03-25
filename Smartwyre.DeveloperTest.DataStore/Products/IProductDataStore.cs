namespace Smartwyre.DeveloperTest.DataStore.Products
{
    public interface IProductDataStore
    {
        Task<Product?> GetProductAsync(string productIdentifier);
    }
}
