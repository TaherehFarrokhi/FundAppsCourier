using System;
using System.Collections.Generic;
using CourierService.Models;

namespace CourierService
{
    public interface IParcelPriceCalculator
    {
        ParcelCalculationResult Calculate(Parcel parcel, Action<DeliveryOptions> configureOptions = null);

        ParcelCalculationsResult Calculate(IEnumerable<Parcel> parcels,
            Action<DeliveryOptions> configureOptions = null);
    }
}