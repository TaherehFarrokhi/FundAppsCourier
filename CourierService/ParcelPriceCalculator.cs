using System;
using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelPriceCalculator : IParcelPriceCalculator
    {
        private readonly IParcelMetadataProvider _parcelMetadataProvider;
        private readonly IWeightCostCalculator _weightCostCalculator;

        public ParcelPriceCalculator(IParcelMetadataProvider parcelMetadataProvider,
            IWeightCostCalculator weightCostCalculator)
        {
            _parcelMetadataProvider =
                parcelMetadataProvider ?? throw new ArgumentNullException(nameof(parcelMetadataProvider));
            _weightCostCalculator =
                weightCostCalculator ?? throw new ArgumentNullException(nameof(weightCostCalculator));
        }

        public ParcelCalculationResult Calculate(Parcel parcel)
        {
            return Calculate(parcel, new DeliveryOptions(false));
        }

        public ParcelCalculationResult Calculate(Parcel parcel, DeliveryOptions deliveryOptions)
        {
            if (parcel == null) throw new ArgumentNullException(nameof(parcel));
            var parcelMetadata = _parcelMetadataProvider.ResolveParcelMetadata(parcel);

            var cost = parcelMetadata.Cost + _weightCostCalculator.GetCost(parcel.Weight, parcelMetadata.WightLimit);

            var fastDeliveryCost = deliveryOptions.FastDelivery ? cost : 0;

            return new ParcelCalculationResult(cost, fastDeliveryCost);
        }
    }
}