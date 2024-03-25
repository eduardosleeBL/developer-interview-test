using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.StrategyRebateCalculators.RebateCalculatorsTypes
{
    public class FixedCashAmountCalculator : IRebateCalculator
    {
        public CalculateRebateResult Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            var result = new CalculateRebateResult();

            if (rebate == null)
            {
                result.Success = false;
            }
            else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
            {
                result.Success = false;
            }
            else if (rebate.Amount == 0)
            {
                result.Success = false;
            }
            else
            {
                result.RebateAmount = rebate.Amount;
                result.Success = true;
            }

            return result;
        }
}
}
