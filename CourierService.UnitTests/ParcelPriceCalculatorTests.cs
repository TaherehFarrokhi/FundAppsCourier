using CourierService.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourierService.UnitTests
{
    public class ParcelPriceCalculatorTests
    {
        [Fact]
        public void Given_Parcel_Should_ReturnValidResult()
        {
            //Arrange
            var sut = new ParcelPriceCalculator(new ParcelMetadataProvider(), new WeightCostCalculator());
            var parcel = new Parcel(0, 0, 0, 0);

            //Act
            var result = sut.Calculate(parcel);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void Given_Parcel_Should_ReturnValidResultWithCorrectTotalPrice()
        {
            //Arrange
            var parcelMetadataProvider = new Mock<IParcelMetadataProvider>();
            parcelMetadataProvider.Setup(x => x.ResolveParcelMetadata(It.IsAny<Parcel>()))
                .Returns(new ParcelMetadata(ParcelType.Small, 10, 10, 2));

            var weightCostCalculator = new Mock<IWeightCostCalculator>();
            weightCostCalculator.Setup(y => y.GetCost(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(20);

            var sut = new ParcelPriceCalculator(parcelMetadataProvider.Object, weightCostCalculator.Object);
            var parcel = new Parcel(10, 10, 10, 10);

            //Act
            var result = sut.Calculate(parcel);

            //Assert
            result.Should().NotBeNull();
            result.DeliveryCost.Should().Be(30);
            result.TotalCost.Should().Be(30);
            result.FastDeliveryCost.Should().Be(0);
        }

        [Fact]
        public void Given_ParcelWithFastDeliveryOption_Should_ReturnValidResultWithCorrectTotalPrice()
        {
            //Arrange
            var parcelMetadataProvider = new Mock<IParcelMetadataProvider>();
            parcelMetadataProvider.Setup(x => x.ResolveParcelMetadata(It.IsAny<Parcel>()))
                .Returns(new ParcelMetadata(ParcelType.Small, 10, 10, 2));

            var weightCostCalculator = new Mock<IWeightCostCalculator>();
            weightCostCalculator.Setup(y => y.GetCost(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(20);

            var sut = new ParcelPriceCalculator(parcelMetadataProvider.Object, weightCostCalculator.Object);
            var parcel = new Parcel(10, 10, 10, 10);

            //Act
            var result = sut.Calculate(parcel, new DeliveryOptions(true));

            //Assert
            result.Should().NotBeNull();
            result.DeliveryCost.Should().Be(30);
            result.TotalCost.Should().Be(60);
            result.FastDeliveryCost.Should().Be(30);
        }
    }
}