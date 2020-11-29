using System.Collections.Generic;
using CourierService.Models;

namespace CourierService.Discounts
{
    public interface IDiscountCalculator
    {
        IEnumerable<DeliveryDiscount> CalculateDiscount(IEnumerable<ParcelDeliveryCost> parcelDeliveryCosts);
    }
}