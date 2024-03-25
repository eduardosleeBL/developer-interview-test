using Smartwyre.DeveloperTest.DataStore.Rebates;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.StrategyRebateCalculators
{
    public interface IRebateCalculatorRegister
    {
        Dictionary<IncentiveType, IRebateCalculator> GetRebateCalculators();
    }
}
