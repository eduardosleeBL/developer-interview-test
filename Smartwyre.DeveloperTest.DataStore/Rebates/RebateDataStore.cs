using Microsoft.EntityFrameworkCore;
using Smartwyre.DeveloperTest.DataStore.Data;
using Smartwyre.DeveloperTest.DataStore.RebateCalculations;

namespace Smartwyre.DeveloperTest.DataStore.Rebates;

public class RebateDataStore : IRebateDataStore
{
    private readonly SmartwyreDbContext _smartwyreDbContext;

    public RebateDataStore(SmartwyreDbContext smartwyreDbContext)
    {
        _smartwyreDbContext = smartwyreDbContext;
    }

    public async Task<Rebate?> GetRebateAsync(string rebateIdentifier)
    {
        return await _smartwyreDbContext.Rebates
                .FirstOrDefaultAsync(rebate => rebate.Identifier == rebateIdentifier);
    }

    public async Task StoreCalculationResultAsync(Rebate account, decimal rebateAmount)
    {
        var rebateCalculation = new RebateCalculation
        {
            Identifier = Guid.NewGuid().ToString(),
            RebateIdentifier = account.Identifier,
            IncentiveType = account.Incentive,
            Amount = rebateAmount
        };

        _smartwyreDbContext.RebateCalculations.Add(rebateCalculation);
        await _smartwyreDbContext.SaveChangesAsync();
    }
}
