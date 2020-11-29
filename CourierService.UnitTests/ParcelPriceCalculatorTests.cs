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
            var parcel = new Parcel(width, length, height, 0);

            //Act
            var result = sut.Calculate(parcel);

            //Assert
            result.Should().NotBeNull();
            result.DeliveryCost.Should().Be(expected);
            result.TotalCost.Should().Be(expected);
        }

        
        [Theory]
        [InlineData(8, true, 6)]
        [InlineData(8, false, 3)]
        [InlineData(10, true, 16)]
        [InlineData(10, false, 8)]
        [InlineData(50, true, 30)]
        [InlineData(50, false, 15)]
        [InlineData(100, true, 50)]
        [InlineData(100, false, 25)]
        public void Given_ParcelWithSpecificSizeAndFastDelivery_Should_ReturnValidResultWithCorrectTotalPrice(decimal dimension, bool fastDelivery, decimal expectedCost)
        {
            //Arrange
            var sut = new ParcelPriceCalculator(new ParcelCostProvider());
            var parcel = new Parcel(dimension, dimension, dimension, 0);

            //Act
            var result = sut.Calculate(parcel, new DeliveryOptions(fastDelivery));

            //Assert
            result.Should().NotBeNull();
            result.TotalCost.Should().Be(expectedCost);
            result.FastDeliveryCost.Should().Be(fastDelivery ? result.DeliveryCost: 0);
        }
        
        [Fact]
        public void Given_Parcel_Should_ReturnValidResult()
        {
            //Arrange
            var sut = new ParcelPriceCalculator(new ParcelCostProvider());
            var parcel = new Parcel(0, 0, 0, 0);

            //Act
            var result = sut.Calculate(parcel);

            //Assert
            result.Should().NotBeNull();
        }
    }
}