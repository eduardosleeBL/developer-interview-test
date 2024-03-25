using Microsoft.EntityFrameworkCore;
using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.RebateCalculations;
using Smartwyre.DeveloperTest.DataStore.Rebates;

namespace Smartwyre.DeveloperTest.DataStore.Data
{
    public class SmartwyreDbContext : DbContext
    {
        public SmartwyreDbContext(DbContextOptions<SmartwyreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Rebate> Rebates { get; set; }

        public DbSet<RebateCalculation> RebateCalculations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new RebateConfiguration());
            modelBuilder.ApplyConfiguration(new RebateCalculationConfiguration());
        }
    }
}
