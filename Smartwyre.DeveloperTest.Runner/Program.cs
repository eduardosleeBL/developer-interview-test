using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartwyre.DeveloperTest.DataStore.Data;
using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.StrategyRebateCalculators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = CreateHostBuilder().Build();
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;

        try
        {
            await CreateProductsAndRebates(services);
            await services.GetRequiredService<App>().Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            
            .ConfigureServices((_, services) =>
            {
                services.AddDbContext<SmartwyreDbContext>(
                    options => options.UseSqlServer("Data Source=localhost;Initial Catalog=Smartwyre;User ID=sa;Password=Password1!;TrustServerCertificate=True;"));
                services.AddScoped<IProductDataStore, ProductDataStore>();
                services.AddScoped<IRebateDataStore, RebateDataStore>();
                services.AddScoped<IRebateService, RebateService>();
                services.AddScoped<IRebateCalculatorRegister, RebateCalculatorRegister>();
                services.AddScoped<IRebateCalculators, RebateCalculators>();
                services.AddScoped<App>();
            });
    }

    private static async Task CreateProductsAndRebates(IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var smartwyreDbContext = scope.ServiceProvider.GetRequiredService<SmartwyreDbContext>();
            smartwyreDbContext.Database.EnsureCreated();

            if (!smartwyreDbContext.Products.Any())
            {
                var product1 = new Product
                {
                    Identifier = "product1",
                    Price = 1,
                    SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                    Uom = "Uom1"
                };
                var product2 = new Product
                {
                    Identifier = "product2",
                    Price = 10,
                    SupportedIncentives = SupportedIncentiveType.FixedCashAmount,
                    Uom = "Uom2"
                };

                await smartwyreDbContext.Products.AddRangeAsync(product1, product2);
                await smartwyreDbContext.SaveChangesAsync();
            }

            if (!smartwyreDbContext.Rebates.Any())
            {
                var rebate1 = new Rebate
                {
                    Identifier = "rebate1",
                    Amount = 1,
                    Incentive = IncentiveType.FixedRateRebate,
                    Percentage = 10
                };
                var rebate2 = new Rebate
                {
                    Identifier = "rebate2",
                    Amount = 10,
                    Incentive = IncentiveType.FixedCashAmount,
                    Percentage = 20
                };

                await smartwyreDbContext.Rebates.AddRangeAsync(rebate1, rebate2);
                await smartwyreDbContext.SaveChangesAsync();
            }
        }
    }
}
