using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.StrategyRebateCalculators
{
    public class RebateCalculators : IRebateCalculators
    {
        private readonly Dictionary<IncentiveType, IRebateCalculator> _rebateCalculatorsDictionary;

        public RebateCalculators(IRebateCalculatorRegister rebateCalculatorRegister)
        {
            _rebateCalculatorsDictionary = rebateCalculatorRegister.GetRebateCalculators();
        }

        public CalculateRebateResult RebateCalculate(
            Rebate rebate, Product product, CalculateRebateRequest request)
        {
            if (_rebateCalculatorsDictionary.TryGetValue(rebate.Incentive, out var rebateCalculator))
            {
                return rebateCalculator.Calculate(rebate, product, request);
            }

            throw new KeyNotFoundException("The rebate calculator type is not valid.");
        }
    }
}
