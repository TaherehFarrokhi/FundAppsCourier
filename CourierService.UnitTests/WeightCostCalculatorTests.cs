using FluentAssertions;
using Xunit;

namespace CourierService.UnitTests
{
    public class WeightCostCalculatorTests
    {
        [Theory]
        [InlineData(1.5, 1, 2)]
        [InlineData(10.5, 1, 20)]
        [InlineData(10.5, 3, 16)]
        [InlineData(10.5, 6, 10)]
        public void Given_WeightAndWeightLimit_Should_ReturnExpectedCost(decimal weight, decimal weightLimit,
            decimal expectedCost)
        {
            //Arrange
            var sut = new WeightCostCalculator();

            //Act
            var result = sut.GetCost(weight, weightLimit);

            //Assert
            result.Should().Be(expectedCost);
        }
    }
}