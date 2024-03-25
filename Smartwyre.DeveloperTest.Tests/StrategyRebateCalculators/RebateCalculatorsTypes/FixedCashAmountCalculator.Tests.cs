using FluentAssertions;
using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.StrategyRebateCalculators.RebateCalculatorsTypes;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.StrategyRebateCalculators.RebateCalculatorsTypes
{
    public class FixedCashAmountCalculatorTests
    {
        private readonly FixedCashAmountCalculator _fixedCashAmountCalculator;

        public FixedCashAmountCalculatorTests()
        {
            _fixedCashAmountCalculator = new FixedCashAmountCalculator();
        }

        [Fact]
        public void Calculate_WhenRebateIsNull_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = (Rebate)null;
            var product = new Product();
            var request = new CalculateRebateRequest();

            var result = _fixedCashAmountCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenSupportedIncentivesHasNoFlagToFixedCashAmount_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = new Rebate();
            var product = new Product() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
            var request = new CalculateRebateRequest();

            var result = _fixedCashAmountCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenAmountIsEqualToZero_ShouldReturnSuccessEqualToFalse()
        {
            var rebate = new Rebate() { Amount = 0 };
            var product = new Product() { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest();

            var result = _fixedCashAmountCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void Calculate_WhenParametersAreValid_ShouldReturnValidResult()
        {
            var rebate = new Rebate() { Amount = 10 };
            var product = new Product() { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest() { Volume = 10 };

            var result = _fixedCashAmountCalculator.Calculate(rebate, product, request);

            result.Success.Should().BeTrue();
            result.RebateAmount.Should().Be(rebate.Amount);
        }
    }
}
