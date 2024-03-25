using FluentAssertions;
using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.StrategyRebateCalculators.RebateCalculatorsTypes;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.StrategyRebateCalculators.RebateCalculatorsTypes
{
    public class FixedRateRebateCalculatorTests
    {
        private readonly FixedRateRebateCalculator _fixedRateRebateCalculator;

        public FixedRateRebateCalculatorTests()
        {
            _fixedRateRebateCalculator = new FixedRateRebateCalculator();
        }

        [Fact]
        public void Calculate_WhenRebateIsNull_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = (Rebate)null;
            var product = new Product();
            var request = new CalculateRebateRequest();

            var result = _fixedRateRebateCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenProductIsNull_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = new Rebate();
            var product = (Product)null;
            var request = new CalculateRebateRequest();

            var result = _fixedRateRebateCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenSupportedIncentivesHasNoFlagToFixedRateRebate_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = new Rebate();
            var product = new Product() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
            var request = new CalculateRebateRequest();

            var result = _fixedRateRebateCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenPercentageAndPriceAndVolumeAreEqualToZero_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = new Rebate() { Percentage = 0 };
            var product = new Product() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 0 };
            var request = new CalculateRebateRequest() { Volume = 0 };

            var result = _fixedRateRebateCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenParametersAreValid_ShouldReturnValidResult()
        {
            var rebate = new Rebate() { Percentage = 10 };
            var product = new Product() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 5 };
            var request = new CalculateRebateRequest() { Volume = 20 };
            var rebateAmount = product.Price * rebate.Percentage * request.Volume;

            var result = _fixedRateRebateCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeTrue();
            result.RebateAmount.Should().Be(rebateAmount);
        }
    }
}
