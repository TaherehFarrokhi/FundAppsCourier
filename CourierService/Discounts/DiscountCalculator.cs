using System.Collections.Generic;
using CourierService.Models;

namespace CourierService.Discounts
{
    public sealed class DiscountCalculator : IDiscountCalculator
    {
        private readonly IEnumerable<IDiscountStrategy> _strategies = new[]
        {
            new NthFreeDiscountStrategy(3, ParcelType.Medium),
            new NthFreeDiscountStrategy(4, ParcelType.Small),
            new NthFreeDiscountStrategy(5)
        };

        public IEnumerable<DeliveryDiscount> CalculateDiscount(IEnumerable<ParcelDeliveryCost> parcelDeliveryCosts)
        {
            var remaining = parcelDeliveryCosts;
            var deliveryDiscounts = new List<DeliveryDiscount>();

            foreach (var discountStrategy in _strategies)
            {
                var (discounts, remainingCosts) =
                    discountStrategy.CalculateDiscount(remaining);

                remaining = remainingCosts;
                deliveryDiscounts.AddRange(discounts);
            }

            return deliveryDiscounts;
        }
    }
}