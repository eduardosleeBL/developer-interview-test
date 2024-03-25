using FluentAssertions;
using Moq;
using Smartwyre.DeveloperTest.DataStore.Products;
using Smartwyre.DeveloperTest.DataStore.Rebates;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.StrategyRebateCalculators;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class RebateServiceTests
{
    private readonly Mock<IRebateDataStore> _rebateDataStoreMock;
    private readonly Mock<IProductDataStore> _productDataStoreMock;
    private readonly Mock<IRebateCalculators> _rebateCalculatorsMock;
    private readonly RebateService _rebateService;

    public RebateServiceTests()
    {
        _rebateDataStoreMock = new Mock<IRebateDataStore>();
        _productDataStoreMock = new Mock<IProductDataStore>();
        _rebateCalculatorsMock = new Mock<IRebateCalculators>();

        _rebateService = new RebateService(
            _rebateDataStoreMock.Object,
            _productDataStoreMock.Object,
            _rebateCalculatorsMock.Object);
    }

    [Fact]
    public async Task CalculateRebateAsync_WhenRequestIsNull_ShouldReturnArgumentNullException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _rebateService
            .CalculateRebateAsync(default));

        exception.Message.Should().Be("Value cannot be null. (Parameter 'request')");
    }

    [Fact]
    public async Task CalculateRebateAsync_WhenRebateIdentifierPropertyIsNull_ShouldReturnKeyNotFoundException()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = null
        };

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _rebateService
            .CalculateRebateAsync(request));

        exception.Message.Should().Be("Rebate not found.");
    }

    [Fact]
    public async Task CalculateRebateAsync_WhenProductIdentifierPropertyIsNull_ShouldReturnKeyNotFoundException()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = string.Empty,
            ProductIdentifier = null
        };

        var expectedRebate = new Rebate();

        _rebateDataStoreMock.Setup(rebateDataStore => rebateDataStore.GetRebateAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedRebate);

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _rebateService
            .CalculateRebateAsync(request));

        exception.Message.Should().Be("Product not found.");
    }

    [Fact]
    public async Task CalculateRebateAsync_WhenCalculateResultIsSuccess_ShouldStoreCalculationResultAsync()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = string.Empty,
            ProductIdentifier = string.Empty
        };

        var expectedRebate = new Rebate()
        {
            Identifier = "rebate1"
        };

        var expectedProduct = new Product();
        var expectedCalculateRebateResult = new CalculateRebateResult()
        {
            Success = true,
            RebateAmount = 100
        };

        _rebateDataStoreMock.Setup(rebateDataStore => rebateDataStore.GetRebateAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedRebate);

        _productDataStoreMock.Setup(productDataStore => productDataStore.GetProductAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedProduct);

        _rebateCalculatorsMock.Setup(rebateCalculators => rebateCalculators.RebateCalculate(expectedRebate, expectedProduct, request))
            .Returns(expectedCalculateRebateResult);

        var result = await _rebateService.CalculateRebateAsync(request);

        _rebateDataStoreMock.Verify(rebateDataStore => rebateDataStore
            .StoreCalculationResultAsync(It.Is<Rebate>(rebate => rebate.Identifier == expectedRebate.Identifier),
                expectedCalculateRebateResult.RebateAmount), Times.Once());

        result.Should().NotBeNull();
        result.Success.Should().Be(expectedCalculateRebateResult.Success);
        result.RebateAmount.Should().Be(expectedCalculateRebateResult.RebateAmount);
    }

    [Fact]
    public async Task CalculateRebateAsync_WhenCalculateResultIsNotSuccess_ShouldNotStoreCalculationResultAsync()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = string.Empty,
            ProductIdentifier = string.Empty
        };

        var expectedRebate = new Rebate()
        {
            Identifier = "rebate1"
        };

        var expectedProduct = new Product();
        var expectedCalculateRebateResult = new CalculateRebateResult()
        {
            Success = false,
            RebateAmount = 100
        };

        _rebateDataStoreMock.Setup(rebateDataStore => rebateDataStore.GetRebateAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedRebate);

        _productDataStoreMock.Setup(productDataStore => productDataStore.GetProductAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedProduct);

        _rebateCalculatorsMock.Setup(rebateCalculators => rebateCalculators.RebateCalculate(expectedRebate, expectedProduct, request))
            .Returns(expectedCalculateRebateResult);

        var result = await _rebateService.CalculateRebateAsync(request);

        _rebateDataStoreMock.Verify(rebateDataStore => rebateDataStore
            .StoreCalculationResultAsync(It.Is<Rebate>(rebate => rebate.Identifier == expectedRebate.Identifier),
                expectedCalculateRebateResult.RebateAmount), Times.Never);

        result.Should().NotBeNull();
        result.Success.Should().Be(expectedCalculateRebateResult.Success);
        result.RebateAmount.Should().Be(expectedCalculateRebateResult.RebateAmount);
    }
}