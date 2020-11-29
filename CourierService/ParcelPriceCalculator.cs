using System;
using System.Collections.Generic;
using System.Linq;
using CourierService.Discounts;
using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelPriceCalculator : IParcelPriceCalculator
    {
        private readonly IDiscountCalculator _discountCalculator;
        private readonly IParcelMetadataProvider _parcelMetadataProvider;
        private readonly IWeightCostCalculator _weightCostCalculator;

        public ParcelPriceCalculator(IParcelMetadataProvider parcelMetadataProvider,
            IDiscountCalculator discountCalculator,
            IWeightCostCalculator weightCostCalculator)
        {
            _parcelMetadataProvider =
                parcelMetadataProvider ?? throw new ArgumentNullException(nameof(parcelMetadataProvider));
            _discountCalculator = discountCalculator ?? throw new ArgumentNullException(nameof(discountCalculator));
            _weightCostCalculator =
                weightCostCalculator ?? throw new ArgumentNullException(nameof(weightCostCalculator));
        }

        public ParcelCalculationResult Calculate(Parcel parcel, Action<DeliveryOptions> configureOptions = null)
        {
            if (parcel == null) throw new ArgumentNullException(nameof(parcel));

            var deliveryOptions = new DeliveryOptions(false);
            configureOptions?.Invoke(deliveryOptions);

            var (_, cost) = CalculateParcelCost(parcel);
            var fastDeliveryCost = CalculateFastDeliveryCost(deliveryOptions, cost);

            return new ParcelCalculationResult(cost, fastDeliveryCost);
        }

        public ParcelCalculationsResult Calculate(IEnumerable<Parcel> parcels,
            Action<DeliveryOptions> configureOptions = null)
        {
            if (parcels == null) throw new ArgumentNullException(nameof(parcels));

            var deliveryOptions = new DeliveryOptions(false);
            configureOptions?.Invoke(deliveryOptions);

            var parcelCosts = parcels
                .Select((p, i) =>
                {
                    var (metadata, cost) = CalculateParcelCost(p);

                    return new ParcelDeliveryCost($"Parcel #{i}, {metadata.ParcelType.ToString()} Parcel",
                        metadata.ParcelType, cost);
                })
                .ToArray();

            var deliveryDiscounts = _discountCalculator.CalculateDiscount(parcelCosts).ToArray();

            var costAfterDiscount = parcelCosts.Sum(pc => pc.Cost) - deliveryDiscounts.Sum(d => d.Discount);

            var fastDeliveryCost = CalculateFastDeliveryCost(deliveryOptions, costAfterDiscount);

            return new ParcelCalculationsResult(parcelCosts, deliveryDiscounts, fastDeliveryCost);
        }

        private (ParcelMetadata metadata, decimal Cost) CalculateParcelCost(Parcel parcel)
        {
            var parcelMetadata = _parcelMetadataProvider.ResolveParcelMetadata(parcel);

            var cost = parcelMetadata.Cost + _weightCostCalculator.GetCost(parcel.Weight, parcelMetadata.WightLimit,
                parcelMetadata.OverweightCost);

            return (parcelMetadata, cost);
        }

        private decimal CalculateFastDeliveryCost(DeliveryOptions deliveryOptions, decimal cost)
        {
            return deliveryOptions.FastDelivery ? cost : 0;
        }
    }
}