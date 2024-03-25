using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.StrategyRebateCalculators.RebateCalculatorsTypes
{
    public class FixedRateRebateCalculator : IRebateCalculator
    {
        public CalculateRebateResult Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            var result = new CalculateRebateResult();

            if (rebate == null)
            {
                result.Success = false;
            }
            else if (product == null)
            {
                result.Success = false;
            }
            else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
            {
                result.Success = false;
            }
            else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
            {
                result.Success = false;
            }
            else
            {
                result.RebateAmount += product.Price * rebate.Percentage * request.Volume;
                result.Success = true;
            }

            return result;
        }
    }
}
