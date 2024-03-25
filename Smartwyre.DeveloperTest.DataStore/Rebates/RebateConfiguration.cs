using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Smartwyre.DeveloperTest.DataStore.Rebates
{
    public class RebateConfiguration : IEntityTypeConfiguration<Rebate>
    {
        public void Configure(EntityTypeBuilder<Rebate> entity)
        {
            entity.Property(e => e.Percentage).HasDefaultValue(0);
            entity.Property(e => e.Amount).HasDefaultValue(0);
            entity.HasIndex(e => e.Identifier).IsUnique(true);
        }
    }
}
