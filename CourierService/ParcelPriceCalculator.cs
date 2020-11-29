using System;
using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelPriceCalculator : IParcelPriceCalculator
    {
        private readonly IParcelTypeCalculator _parcelTypeCalculator;

        public ParcelPriceCalculator(IParcelTypeCalculator parcelTypeCalculator)
        {
            _parcelTypeCalculator = parcelTypeCalculator ?? throw new ArgumentNullException(nameof(parcelTypeCalculator));
        }
        public ParcelCalculationResult Calculate(Parcel parcel)
        {
            if (parcel == null) throw new ArgumentNullException(nameof(parcel));
            var parcelType = _parcelTypeCalculator.GetParcelType(parcel);
            
            return parcelType switch
            {
                ParcelType.Small => new ParcelCalculationResult(3m),
                ParcelType.Medium => new ParcelCalculationResult(8m),
                ParcelType.Large => new ParcelCalculationResult(15m),
                ParcelType.XLarge => new ParcelCalculationResult(25m),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}