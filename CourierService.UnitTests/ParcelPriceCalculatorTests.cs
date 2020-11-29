using CourierService.Models;
using FluentAssertions;
using Xunit;

namespace CourierService.UnitTests
{
    public class ParcelPriceCalculatorTests
    {
        [Theory]
        [InlineData(8, 8, 9, 3)]
        [InlineData(10, 9, 9, 8)]
        [InlineData(7, 10, 9, 8)]
        [InlineData(7, 5, 10, 8)]
        [InlineData(11, 15, 10, 8)]
        [InlineData(50, 9, 9, 15)]
        [InlineData(30, 50, 9, 15)]
        [InlineData(20, 30, 70, 15)]
        [InlineData(50, 99, 80, 15)]
        [InlineData(99, 99, 99, 15)]
        [InlineData(100, 9, 9, 25)]
        [InlineData(99, 100, 9, 25)]
        [InlineData(60, 70, 100, 25)]
        [InlineData(100, 1, 1, 25)]
        [InlineData(150, 100, 780, 25)]
        public void Given_ParcelWithSpecificSize_Should_ReturnValidResultWithCorrectTotalPrice(decimal width,
            decimal length, decimal height, decimal expected)
        {
            //Arrange
            var sut = new ParcelPriceCalculator(new ParcelCostProvider());
            var parcel = new Parcel(width, length, height);

            //Act
            var result = sut.Calculate(parcel);

            //Assert
            result.Should().NotBeNull();
            result.TotalCost.Should().Be(expected);
        }

        [Fact]
        public void Given_Parcel_Should_ReturnValidResult()
        {
            //Arrange
            var sut = new ParcelPriceCalculator(new ParcelCostProvider());
            var parcel = new Parcel(0, 0, 0);

            //Act
            var result = sut.Calculate(parcel);

            //Assert
            result.Should().NotBeNull();
        }
    }
}