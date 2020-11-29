using System.Collections.Generic;
using CourierService.Models;

namespace CourierService.Discounts
{
    public interface IDiscountStrategy
    {
        (IEnumerable<DeliveryDiscount> Discounts, IEnumerable<ParcelDeliveryCost> RemainingCosts) CalculateDiscount(
            IEnumerable<ParcelDeliveryCost> parcelDeliveryCosts);
    }
}