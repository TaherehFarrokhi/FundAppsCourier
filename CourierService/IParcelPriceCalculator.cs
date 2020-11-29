using System;
using CourierService.Models;

namespace CourierService
{
    public interface IParcelPriceCalculator
    {
        ParcelCalculationResult Calculate(Parcel parcel, Action<DeliveryOptions> configureOptions = null);
    }
}