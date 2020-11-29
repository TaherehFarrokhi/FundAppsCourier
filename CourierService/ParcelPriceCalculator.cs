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
            if (parcel == null) throw new ArgumentNullException(nameof(parcel));
            var (_, cost) = _parcelCostProvider.ResolveParcelCost(parcel);

            return new ParcelCalculationResult(cost);
        }
    }
}