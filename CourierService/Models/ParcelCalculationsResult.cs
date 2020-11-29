using System.Collections.Generic;
using System.Linq;

namespace CourierService.Models
{
    public sealed class ParcelCalculationsResult
    {
        public ParcelCalculationsResult(IEnumerable<ParcelDeliveryCost> parcelDeliveryCosts,
            IEnumerable<DeliveryDiscount> deliveryDiscounts, decimal fastDeliveryCost)
        {
            ParcelDeliveryCosts = parcelDeliveryCosts ?? new ParcelDeliveryCost[] { };
            DeliveryDiscounts = deliveryDiscounts ?? new DeliveryDiscount[] { };
            FastDeliveryCost = fastDeliveryCost;
        }

        public IEnumerable<ParcelDeliveryCost> ParcelDeliveryCosts { get; }
        public IEnumerable<DeliveryDiscount> DeliveryDiscounts { get; }
        public decimal FastDeliveryCost { get; }

        public decimal TotalCost => ParcelDeliveryCosts.Sum(m => m.Cost)
            - DeliveryDiscounts.Sum(m => m.Discount) + FastDeliveryCost;
    }
}