using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.StrategyRebateCalculators;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateCalculators _rebateCalculators;

    public RebateService(
        IRebateDataStore rebateDataStore,
        IProductDataStore productDataStore,
        IRebateCalculators rebateCalculators)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _rebateCalculators = rebateCalculators;
    }

    public async Task<CalculateRebateResult> CalculateRebateAsync(CalculateRebateRequest request)
    {
        if(request is null)
            throw new ArgumentNullException(nameof(request));

        var rebate = await _rebateDataStore.GetRebateAsync(request.RebateIdentifier);

        if (rebate is null)
            throw new KeyNotFoundException("Rebate not found.");

        var product = await _productDataStore.GetProductAsync(request.ProductIdentifier);

        if (product is null)
            throw new KeyNotFoundException("Product not found.");

        var result = _rebateCalculators.RebateCalculate(rebate, product, request);

        if (result.Success)
        {
            await _rebateDataStore.StoreCalculationResultAsync(rebate, result.RebateAmount);
        }

        return result;
    }
}
