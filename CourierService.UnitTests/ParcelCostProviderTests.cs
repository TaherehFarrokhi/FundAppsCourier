using CourierService.Models;
using FluentAssertions;
using Xunit;

namespace CourierService.UnitTests
{
    public class ParcelCostProviderTests
    {
        [Theory]
        [InlineData(8, 8, 9, ParcelType.Small, 3)]
        [InlineData(10, 9, 9, ParcelType.Medium, 8)]
        [InlineData(7, 10, 9, ParcelType.Medium, 8)]
        [InlineData(7, 5, 10, ParcelType.Medium, 8)]
        [InlineData(11, 15, 10, ParcelType.Medium, 8)]
        [InlineData(50, 9, 9, ParcelType.Large, 15)]
        [InlineData(30, 50, 9, ParcelType.Large, 15)]
        [InlineData(20, 30, 70, ParcelType.Large, 15)]
        [InlineData(50, 99, 80, ParcelType.Large, 15)]
        [InlineData(99, 99, 99, ParcelType.Large, 15)]
        [InlineData(100, 9, 9, ParcelType.XLarge, 25)]
        [InlineData(99, 100, 9, ParcelType.XLarge, 25)]
        [InlineData(60, 70, 100, ParcelType.XLarge, 25)]
        [InlineData(100, 1, 1, ParcelType.XLarge, 25)]
        [InlineData(150, 100, 780, ParcelType.XLarge, 25)]
        public void Given_ParcelWithSpecificSize_Should_ReturnValidResultWithCorrectParcelTypeAndCost(decimal width,
            decimal length, decimal height, ParcelType expectedParcelType, decimal expectedCost)
        {
            //Arrange
            var sut = new ParcelCostProvider();
            var parcel = new Parcel(width, length, height, 0);

            //Act
            var result = sut.ResolveParcelCost(parcel);

            //Assert
            result.Should().NotBeNull();
            result.Should().Be((expectedParcelType, expectedCost));
        }
    }
}