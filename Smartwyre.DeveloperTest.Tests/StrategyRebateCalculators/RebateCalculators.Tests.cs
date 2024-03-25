using FluentAssertions;
using Moq;
using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.StrategyRebateCalculators;
using Smartwyre.DeveloperTest.StrategyRebateCalculators.RebateCalculatorsTypes;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.StrategyRebateCalculators
{
    public class RebateCalculatorsTests
    {
        private readonly Mock<IRebateCalculatorRegister> _rebateCalculatorRegisterMock;
        private RebateCalculators _rebateCalculators;

        public RebateCalculatorsTests()
        {
            _rebateCalculatorRegisterMock = new Mock<IRebateCalculatorRegister>();
        }

        [Fact]
        public void RebateCalculate_WhenSendTheTypeAndExists_ShouldReturnValidResult()
        {
            var rebate = new Rebate() { Incentive = IncentiveType.FixedCashAmount };
            var product = new Product();
            var request = new CalculateRebateRequest();

            _rebateCalculatorRegisterMock.Setup(register => register.GetRebateCalculators())
                .Returns(new Dictionary<IncentiveType, IRebateCalculator>()
                    {
                        { IncentiveType.FixedCashAmount, new FixedCashAmountCalculator() }
                    });

            _rebateCalculators = new RebateCalculators(_rebateCalculatorRegisterMock.Object);

            var result = _rebateCalculators.RebateCalculate(rebate, product, request);

            result.Should().NotBeNull();
        }

        [Fact]
        public void RebateCalculate_WhenTypeIsSentAndNotContainedInTheRegistry_ShouldReturnKeyNotFoundException()
        {
            var rebate = new Rebate();
            var product = new Product();
            var request = new CalculateRebateRequest();

            _rebateCalculatorRegisterMock.Setup(register => register.GetRebateCalculators())
                .Returns(new Dictionary<IncentiveType, IRebateCalculator>() { });

            _rebateCalculators = new RebateCalculators(_rebateCalculatorRegisterMock.Object);

            var exception = Assert.Throws<KeyNotFoundException>(() => _rebateCalculators
                .RebateCalculate(rebate, product, request));

            exception.Message.Should().Be("The rebate calculator type is not valid.");
        }
    }
}
