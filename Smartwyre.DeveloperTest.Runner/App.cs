using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Runner
{
    public class App
    {
        private readonly IRebateService _rebateService;

        public App(IRebateService rebateService)
        {
            _rebateService = rebateService;
        }

        public async Task Run()
        {
            Console.Clear();

            Console.WriteLine("Enter the product identifier:");
            var productIdentifier = Console.ReadLine().Trim();

            Console.WriteLine("Enter the rebate identifier:");
            var rebateIdentifier = Console.ReadLine().Trim();

            Console.WriteLine("Enter the volume:");
            var volume = Convert.ToDecimal(Console.ReadLine());

            var request = new CalculateRebateRequest
            {
                ProductIdentifier = productIdentifier,
                RebateIdentifier = rebateIdentifier,
                Volume = volume
            };

            var result = await _rebateService.CalculateRebateAsync(request);

            Console.Clear();

            if(result.Success)
                Console.WriteLine("Rebate calculation was successfully recorded. New rebate amount is: " + result.RebateAmount);
            else
                Console.WriteLine("There is a problem with the Rebate calculation.");
        }
    }
}
