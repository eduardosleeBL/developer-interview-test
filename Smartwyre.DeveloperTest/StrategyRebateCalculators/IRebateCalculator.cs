using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.StrategyRebateCalculators
{
    public interface IRebateCalculator
    {
        CalculateRebateResult Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
