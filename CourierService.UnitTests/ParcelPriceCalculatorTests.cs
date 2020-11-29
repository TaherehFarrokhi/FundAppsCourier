using System.Collections.Generic;
using CourierService.Discounts;
using CourierService.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourierService.UnitTests
{
    public class ParcelPriceCalculatorTests
    {
        [Fact]
        public void Given_ListOfParcel_Should_ReturnValidResultWithCorrectTotalPrice()
        {
            //Arrange
            var parcel1 = new Parcel(10, 10, 10, 10);
            var parcel2 = new Parcel(10, 10, 10, 60);

            var parcelMetadataProvider = new Mock<IParcelMetadataProvider>();
            parcelMetadataProvider.Setup(x => x.ResolveParcelMetadata(It.Is<Parcel>(p => p.Weight == parcel1.Weight)))
                .Returns(new ParcelMetadata(ParcelType.Small, 10, 10, 2));

            parcelMetadataProvider.Setup(x => x.ResolveParcelMetadata(It.Is<Parcel>(p => p.Weight == parcel2.Weight)))
                .Returns(new ParcelMetadata(ParcelType.Heavy, 50, 50, 1));

            var weightCostCalculator = new Mock<IWeightCostCalculator>();
            weightCostCalculator.Setup(y => y.GetCost(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(20);

            var discountCalculator = new Mock<IDiscountCalculator>();
            discountCalculator.Setup(x => x.CalculateDiscount(It.IsAny<IEnumerable<ParcelDeliveryCost>>()))
                .Returns(new List<DeliveryDiscount>());

            var sut = new ParcelPriceCalculator(parcelMetadataProvider.Object, discountCalculator.Object,
                weightCostCalculator.Object);

            //Act
            var result = sut.Calculate(new[] {parcel1, parcel2}, options => options.WithFastDelivery());

            //Assert
            result.Should().NotBeNull();
            result.DeliveryDiscounts.Should().BeEmpty();
            result.ParcelDeliveryCosts.Should().HaveCount(2)
                .And.Contain(m =>
                    m.ParcelName == "Parcel #0, Small Parcel" && m.ParcelType == ParcelType.Small &&
                    m.Cost == 30m) // Parcel + Overweight
                .And.Contain(m =>
                    m.ParcelName == "Parcel #1, Heavy Parcel" && m.ParcelType == ParcelType.Heavy &&
                    m.Cost == 70m); // Parcel + Overweight

            result.FastDeliveryCost.Should().Be(100);
            result.TotalCost.Should().Be(200);
        }


        [Fact]
        public void
            Given_ListOfParcelsWithFastDeliveryOptionAndEligibleDiscounts_Should_ReturnValidResultWithCorrectTotalPrice()
        {
            //Arrange
            var parcel1 = new Parcel(10, 10, 10, 10);
            var parcel2 = new Parcel(10, 10, 10, 60);

            var parcelMetadataProvider = new Mock<IParcelMetadataProvider>();
            parcelMetadataProvider.Setup(x => x.ResolveParcelMetadata(It.Is<Parcel>(p => p.Weight == parcel1.Weight)))
                .Returns(new ParcelMetadata(ParcelType.Small, 10, 10, 2));

            parcelMetadataProvider.Setup(x => x.ResolveParcelMetadata(It.Is<Parcel>(p => p.Weight == parcel2.Weight)))
                .Returns(new ParcelMetadata(ParcelType.Heavy, 50, 50, 1));

            var weightCostCalculator = new Mock<IWeightCostCalculator>();
            weightCostCalculator.Setup(y => y.GetCost(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(20);

            var discountCalculator = new Mock<IDiscountCalculator>();
            discountCalculator.Setup(x => x.CalculateDiscount(It.IsAny<IEnumerable<ParcelDeliveryCost>>()))
                .Returns(new List<DeliveryDiscount> {new DeliveryDiscount("Delivery Discount for Small Parcel", 5m)});

            var sut = new ParcelPriceCalculator(parcelMetadataProvider.Object, discountCalculator.Object,
                weightCostCalculator.Object);

            //Act
            var result = sut.Calculate(new[] {parcel1, parcel2}, options => options.WithFastDelivery());

            //Assert
            result.Should().NotBeNull();
            result.DeliveryDiscounts.Should().HaveCount(1);
            result.ParcelDeliveryCosts.Should().HaveCount(2)
                .And.Contain(m =>
                    m.ParcelName == "Parcel #0, Small Parcel" && m.ParcelType == ParcelType.Small &&
                    m.Cost == 30m) // Parcel + Overweight
                .And.Contain(m =>
                    m.ParcelName == "Parcel #1, Heavy Parcel" && m.ParcelType == ParcelType.Heavy &&
                    m.Cost == 70m); // Parcel + Overweight

            result.FastDeliveryCost.Should().Be(95);
            result.TotalCost.Should().Be(190);
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

            var discountCalculator = new Mock<IDiscountCalculator>();
            discountCalculator.Setup(x => x.CalculateDiscount(It.IsAny<IEnumerable<ParcelDeliveryCost>>()))
                .Returns(new List<DeliveryDiscount>());

            var sut = new ParcelPriceCalculator(parcelMetadataProvider.Object, discountCalculator.Object,
                weightCostCalculator.Object);
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

            var discountCalculator = new Mock<IDiscountCalculator>();
            discountCalculator.Setup(x => x.CalculateDiscount(It.IsAny<IEnumerable<ParcelDeliveryCost>>()))
                .Returns(new List<DeliveryDiscount>());

            var sut = new ParcelPriceCalculator(parcelMetadataProvider.Object, discountCalculator.Object,
                weightCostCalculator.Object);
            var parcel = new Parcel(10, 10, 10, 10);

            //Act
            var result = sut.Calculate(parcel, d => d.WithFastDelivery());

            //Assert
            result.Should().NotBeNull();
            result.DeliveryCost.Should().Be(30);
            result.TotalCost.Should().Be(60);
            result.FastDeliveryCost.Should().Be(30);
        }
    }
}