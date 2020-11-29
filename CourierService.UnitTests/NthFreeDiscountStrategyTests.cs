using System.Collections.Generic;
using CourierService.Discounts;
using CourierService.Models;
using FluentAssertions;
using Xunit;

namespace CourierService.UnitTests
{
    public class NthFreeDiscountStrategyTests
    {
        [Fact]
        public void
            Given_ParcelTypeListWithCandidateForGivenParcelType_Should_ReturnTheValidDiscountListAndRemainingParcels()
        {
            //Arrange
            var parcelCosts = new List<ParcelDeliveryCost>
            {
                new ParcelDeliveryCost("Parcel #1", ParcelType.Medium, 8),
                new ParcelDeliveryCost("Parcel #2", ParcelType.Medium, 8),
                new ParcelDeliveryCost("Parcel #3", ParcelType.Medium, 8),
                new ParcelDeliveryCost("Parcel #4", ParcelType.Medium, 10),
                new ParcelDeliveryCost("Parcel #5", ParcelType.Medium, 10),
                new ParcelDeliveryCost("Parcel #6", ParcelType.Medium, 10),
                new ParcelDeliveryCost("Parcel #7", ParcelType.Medium, 10),
                new ParcelDeliveryCost("Parcel #8", ParcelType.Medium, 10)
            };

            var sut = new NthFreeDiscountStrategy(3, ParcelType.Medium);

            //Act
            var result = sut.CalculateDiscount(parcelCosts);

            //Assert
            result.Discounts.Should().HaveCount(2)
                .And.Contain(m => m.Discount == 8m && m.Description == "Medium - Discount")
                .And.Contain(m => m.Discount == 10m && m.Description == "Medium - Discount");

            result.RemainingCosts.Should().HaveCount(2)
                .And.Contain(m => m.Cost == 10m && m.ParcelType == ParcelType.Medium && m.ParcelName == "Parcel #7")
                .And.Contain(m => m.Cost == 10m && m.ParcelType == ParcelType.Medium && m.ParcelName == "Parcel #8");
        }

        [Fact]
        public void
            Given_ParcelTypeListWithCandidateWithNoParcelType_Should_ReturnTheValidMixedDiscountListAndRemainingParcels()
        {
            //Arrange
            var parcelCosts = new List<ParcelDeliveryCost>
            {
                new ParcelDeliveryCost("Parcel #1", ParcelType.Medium, 8),
                new ParcelDeliveryCost("Parcel #2", ParcelType.Medium, 8),
                new ParcelDeliveryCost("Parcel #3", ParcelType.Medium, 8),
                new ParcelDeliveryCost("Parcel #4", ParcelType.Medium, 10),
                new ParcelDeliveryCost("Parcel #5", ParcelType.Medium, 10),
                new ParcelDeliveryCost("Parcel #6", ParcelType.Medium, 10),
                new ParcelDeliveryCost("Parcel #7", ParcelType.XLarge, 20),
                new ParcelDeliveryCost("Parcel #8", ParcelType.Large, 15)
            };

            var sut = new NthFreeDiscountStrategy(5);

            //Act
            var result = sut.CalculateDiscount(parcelCosts);

            //Assert
            result.Discounts.Should().HaveCount(1)
                .And.Contain(m => m.Discount == 8m && m.Description == "Mixed - Discount");

            result.RemainingCosts.Should().HaveCount(3)
                .And.Contain(m => m.Cost == 10m && m.ParcelType == ParcelType.Medium && m.ParcelName == "Parcel #6")
                .And.Contain(m => m.Cost == 20m && m.ParcelType == ParcelType.XLarge && m.ParcelName == "Parcel #7")
                .And.Contain(m => m.Cost == 15m && m.ParcelType == ParcelType.Large && m.ParcelName == "Parcel #8");
        }
    }
}