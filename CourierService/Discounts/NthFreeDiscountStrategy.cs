using System.Collections.Generic;
using System.Linq;
using CourierService.Models;

namespace CourierService.Discounts
{
    public class NthFreeDiscountStrategy : IDiscountStrategy
    {
        private readonly int _parcelCount;
        private readonly ParcelType? _parcelType;

        public NthFreeDiscountStrategy(int parcelCount, ParcelType parcelType)
        {
            _parcelType = parcelType;
            _parcelCount = parcelCount;
        }

        public NthFreeDiscountStrategy(int parcelCount)
        {
            _parcelCount = parcelCount;
        }

        public (IEnumerable<DeliveryDiscount> Discounts, IEnumerable<ParcelDeliveryCost> RemainingCosts)
            CalculateDiscount(IEnumerable<ParcelDeliveryCost> parcelDeliveryCosts)
        {
            var deliveryCosts = parcelDeliveryCosts as ParcelDeliveryCost[] ?? parcelDeliveryCosts.ToArray();
            if (!deliveryCosts.Any())
                return (new DeliveryDiscount[] { }, new ParcelDeliveryCost[] { });

            var relevantCosts = deliveryCosts
                .Where(x => _parcelType == null || x.ParcelType == _parcelType)
                .OrderBy(x => x.Cost)
                .Select((x, i) => new {Index = i, Value = x})
                .GroupBy(x => x.Index / _parcelCount)
                .Select(g => g.ToList())
                .Where(x => x.Count == _parcelCount)
                .ToList();

            var deliveryDiscounts = relevantCosts.Select(x =>
            {
                var parcelDeliveryCost = x.First().Value;
                return new DeliveryDiscount($"{_parcelType?.ToString() ?? "Mixed"} - Discount",
                    parcelDeliveryCost.Cost);
            }).ToArray();

            var discountedItems = relevantCosts
                .SelectMany(m => m)
                .Select(m => m.Value)
                .ToArray();

            var remaining = deliveryCosts.Except(discountedItems);

            return (deliveryDiscounts, remaining);
        }
    }
}