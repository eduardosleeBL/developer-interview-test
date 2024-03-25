using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.StrategyRebateCalculators
{
    public interface IRebateCalculators
    {
        CalculateRebateResult RebateCalculate(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
