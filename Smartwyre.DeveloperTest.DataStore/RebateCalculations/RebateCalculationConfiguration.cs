using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Smartwyre.DeveloperTest.DataStore.RebateCalculations
{
    public class RebateCalculationConfiguration : IEntityTypeConfiguration<RebateCalculation>
    {
        public void Configure(EntityTypeBuilder<RebateCalculation> entity)
        {
            entity.Property(e => e.Amount).HasDefaultValue(0);
            entity.HasIndex(e => e.Identifier).IsUnique(true);
        }
    }
}
