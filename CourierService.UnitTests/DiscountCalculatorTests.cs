using System.Collections.Generic;
using System.Linq;
using CourierService.Discounts;
using CourierService.Models;
using FluentAssertions;
using Xunit;

namespace CourierService.UnitTests
{
    public class DiscountCalculatorTests
    {
        [Fact]
        public void Given_ParcelTypeListWithCandidateForMediumDiscount_Should_ReturnTheValidDiscountList()
        {
            //Arrange
            var parcelCosts = new List<ParcelDeliveryCost>
            {
                new ParcelDeliveryCost("Parcel #1", ParcelType.Medium, 8),
                new ParcelDeliveryCost("Parcel #2", ParcelType.Medium, 8),
                new ParcelDeliveryCost("Parcel #3", ParcelType.Medium, 8),
                new ParcelDeliveryCost("Parcel #4", ParcelType.Medium, 10),
                new ParcelDeliveryCost("Parcel #5", ParcelType.Medium, 10),
                new ParcelDeliveryCost("Parcel #6", ParcelType.Medium, 10)
            };

            var sut = new DiscountCalculator();

            //Act
            var result = sut.CalculateDiscount(parcelCosts).ToArray();

            //Assert
            result.Should().HaveCount(2)
                .And.Contain(m => m.Discount == 8m && m.Description == "Medium - Discount")
                .And.Contain(m => m.Discount == 10m && m.Description == "Medium - Discount");
        }

        [Fact]
        public void
            Given_ParcelTypeListWithCandidateForMediumDiscountWithNotEligibleDiscountParcels_Should_ReturnTheValidDiscountList()
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
                // Not Eligible Discounts
                new ParcelDeliveryCost("Parcel #7", ParcelType.Medium, 10),
                new ParcelDeliveryCost("Parcel #8", ParcelType.Medium, 10)
            };

            var sut = new DiscountCalculator();

            //Act
            var result = sut.CalculateDiscount(parcelCosts).ToArray();

            //Assert
            result.Should().HaveCount(2)
                .And.Contain(m => m.Discount == 8m && m.Description == "Medium - Discount")
                .And.Contain(m => m.Discount == 10m && m.Description == "Medium - Discount");
        }

        [Fact]
        public void Given_ParcelTypeListWithCandidateForSmallDiscount_Should_ReturnTheValidDiscountList()
        {
            //Arrange
            var parcelCosts = new List<ParcelDeliveryCost>
            {
                new ParcelDeliveryCost("Parcel #1", ParcelType.Small, 8),
                new ParcelDeliveryCost("Parcel #2", ParcelType.Small, 8),
                new ParcelDeliveryCost("Parcel #3", ParcelType.Small, 8),
                new ParcelDeliveryCost("Parcel #3", ParcelType.Small, 8),
                new ParcelDeliveryCost("Parcel #4", ParcelType.Small, 10),
                new ParcelDeliveryCost("Parcel #5", ParcelType.Small, 10),
                new ParcelDeliveryCost("Parcel #6", ParcelType.Small, 10),
                new ParcelDeliveryCost("Parcel #6", ParcelType.Small, 10)
            };

            var sut = new DiscountCalculator();

            //Act
            var result = sut.CalculateDiscount(parcelCosts).ToArray();

            //Assert
            result.Should().HaveCount(2)
                .And.Contain(m => m.Discount == 8m && m.Description == "Small - Discount")
                .And.Contain(m => m.Discount == 10m && m.Description == "Small - Discount");
        }

        [Fact]
        public void Given_ParcelTypeListWithCandidateForSmallDiscountAndMixedParcels_Should_ReturnTheValidDiscountList()
        {
            //Arrange
            var parcelCosts = new List<ParcelDeliveryCost>
            {
                new ParcelDeliveryCost("Parcel #1", ParcelType.Small, 8),
                new ParcelDeliveryCost("Parcel #2", ParcelType.Small, 8),
                new ParcelDeliveryCost("Parcel #3", ParcelType.Small, 8),
                new ParcelDeliveryCost("Parcel #4", ParcelType.Small, 10),
                new ParcelDeliveryCost("Parcel #7", ParcelType.Large, 20),
                new ParcelDeliveryCost("Parcel #8", ParcelType.XLarge, 25),
                new ParcelDeliveryCost("Parcel #9", ParcelType.XLarge, 25),
                new ParcelDeliveryCost("Parcel #10", ParcelType.Heavy, 50),
                new ParcelDeliveryCost("Parcel #5", ParcelType.Medium, 15),
                new ParcelDeliveryCost("Parcel #6", ParcelType.Medium, 20)
            };

            var sut = new DiscountCalculator();

            //Act
            var result = sut.CalculateDiscount(parcelCosts).ToArray();

            //Assert
            result.Should().HaveCount(2)
                .And.Contain(m => m.Discount == 8m && m.Description == "Small - Discount")
                .And.Contain(m => m.Discount == 15m && m.Description == "Mixed - Discount");
        }

        [Fact]
        public void
            Given_ParcelTypeListWithCandidateForSmallDiscountWithNotEligibleDiscountParcels_Should_ReturnTheValidDiscountList()
        {
            //Arrange
            var parcelCosts = new List<ParcelDeliveryCost>
            {
                new ParcelDeliveryCost("Parcel #1", ParcelType.Small, 8),
                new ParcelDeliveryCost("Parcel #2", ParcelType.Small, 8),
                new ParcelDeliveryCost("Parcel #3", ParcelType.Small, 8),
                new ParcelDeliveryCost("Parcel #4", ParcelType.Small, 10),
                new ParcelDeliveryCost("Parcel #5", ParcelType.Small, 10),
                new ParcelDeliveryCost("Parcel #6", ParcelType.Small, 10),
                new ParcelDeliveryCost("Parcel #7", ParcelType.Small, 10),
                new ParcelDeliveryCost("Parcel #8", ParcelType.Small, 10),
                // Not Eligible Discounts
                new ParcelDeliveryCost("Parcel #9", ParcelType.Small, 10),
                new ParcelDeliveryCost("Parcel #10", ParcelType.Small, 10)
            };

            var sut = new DiscountCalculator();

            //Act
            var result = sut.CalculateDiscount(parcelCosts).ToArray();

            //Assert
            result.Should().HaveCount(2)
                .And.Contain(m => m.Discount == 8m && m.Description == "Small - Discount")
                .And.Contain(m => m.Discount == 10m && m.Description == "Small - Discount");
        }
    }
}