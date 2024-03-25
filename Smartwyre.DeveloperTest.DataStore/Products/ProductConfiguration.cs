using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Smartwyre.DeveloperTest.DataStore.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.Property(e => e.Price).HasDefaultValue(0);
            entity.HasIndex(e => e.Identifier).IsUnique(true);
        }
    }
}
