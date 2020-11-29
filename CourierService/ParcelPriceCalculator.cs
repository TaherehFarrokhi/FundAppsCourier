using System;
using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelPriceCalculator : IParcelPriceCalculator
    {
        private readonly IParcelCostProvider _parcelCostProvider;

        public ParcelPriceCalculator(IParcelCostProvider parcelCostProvider)
        {
            _parcelCostProvider = parcelCostProvider ?? throw new ArgumentNullException(nameof(parcelCostProvider));
        }

        public ParcelCalculationResult Calculate(Parcel parcel)
        {
            return Calculate(parcel, new DeliveryOptions(false));
        }

        public ParcelCalculationResult Calculate(Parcel parcel, DeliveryOptions deliveryOptions)
        {
            if (parcel == null) throw new ArgumentNullException(nameof(parcel));
            var parcelTypeDefinition = _parcelCostProvider.ResolveParcelCost(parcel);

            var fastDeliveryCost = deliveryOptions.FastDelivery ? parcelTypeDefinition.Cost : 0;
            
            return new ParcelCalculationResult(parcelTypeDefinition.Cost, fastDeliveryCost);
        }
    }
}