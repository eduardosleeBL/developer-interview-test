using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.StrategyRebateCalculators.RebateCalculatorsTypes;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.StrategyRebateCalculators
{
    public class RebateCalculatorRegister : IRebateCalculatorRegister
    {
        public Dictionary<IncentiveType, IRebateCalculator> GetRebateCalculators()
        {
            return new Dictionary<IncentiveType, IRebateCalculator>()
            {
                { IncentiveType.FixedCashAmount, new FixedCashAmountCalculator() },
                { IncentiveType.FixedRateRebate, new FixedRateRebateCalculator() },
                { IncentiveType.AmountPerUom, new AmountPerUomCalculator() }
            };
        }
    }
}
