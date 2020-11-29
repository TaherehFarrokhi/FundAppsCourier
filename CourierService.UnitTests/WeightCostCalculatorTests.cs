using FluentAssertions;
using Xunit;

namespace CourierService.UnitTests
{
    public class WeightCostCalculatorTests
    {
        [Theory]
        [InlineData(1.5, 1, 2, 2)]
        [InlineData(10.5, 1, 2, 20)]
        [InlineData(10.5, 3, 2, 16)]
        [InlineData(10.5, 6, 2, 10)]
        [InlineData(1.5, 1, 5, 5)]
        [InlineData(10.5, 1, 5, 50)]
        public void Given_WeightAndWeightLimit_Should_ReturnExpectedCost(decimal weight, decimal weightLimit,
            decimal overweightCost,
            decimal expectedCost)
        {
            //Arrange
            var sut = new WeightCostCalculator();

            //Act
            var result = sut.GetCost(weight, weightLimit, overweightCost);

            //Assert
            result.Should().Be(expectedCost);
        }
    }
}