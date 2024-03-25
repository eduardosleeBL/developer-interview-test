using FluentAssertions;
using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.StrategyRebateCalculators.RebateCalculatorsTypes;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.StrategyRebateCalculators.RebateCalculatorsTypes
{
    public class AmountPerUomCalculatorTests
    {
        private readonly AmountPerUomCalculator _amountPerUomCalculator;

        public AmountPerUomCalculatorTests()
        {
            _amountPerUomCalculator = new AmountPerUomCalculator();
        }

        [Fact]
        public void Calculate_WhenRebateIsNull_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = (Rebate)null;
            var product = new Product();
            var request = new CalculateRebateRequest();

            var result = _amountPerUomCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenProductIsNull_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = new Rebate();
            var product = (Product)null;
            var request = new CalculateRebateRequest();

            var result = _amountPerUomCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenSupportedIncentivesHasNoFlagToAmountPerUom_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = new Rebate();
            var product = new Product() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
            var request = new CalculateRebateRequest();

            var result = _amountPerUomCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenAmountAndVolumeAreEqualToZero_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = new Rebate() { Amount = 0 };
            var product = new Product() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
            var request = new CalculateRebateRequest() { Volume = 0 };

            var result = _amountPerUomCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenParametersAreValid_ShouldReturnValidResult()
        {
            var rebate = new Rebate() { Amount = 10 };
            var product = new Product() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
            var request = new CalculateRebateRequest() { Volume = 10 };
            var rebateAmount = rebate.Amount * request.Volume;

            var result = _amountPerUomCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeTrue();
            result.RebateAmount.Should().Be(rebateAmount);
        }
    }
}
