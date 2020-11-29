using CourierService.Models;
using FluentAssertions;
using Xunit;

namespace CourierService.UnitTests
{
    public class ParcelPriceCalculatorTests
    {
        [Fact]
        public void Given_Parcels_Should_ReturnValidResult()
        {
            //Arrange
            var sut = new ParcelPriceCalculator();
            var parcel = new Parcel(0, 0, 0);

            //Act
            var result = sut.Calculate(parcel);

            //Assert
            result.Should().NotBeNull();
        }
    }
}