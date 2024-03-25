namespace Smartwyre.DeveloperTest.DataStore.Rebates
{
    public interface IRebateDataStore
    {
        Task<Rebate?> GetRebateAsync(string rebateIdentifier);

        Task StoreCalculationResultAsync(Rebate account, decimal rebateAmount);
    }
}
