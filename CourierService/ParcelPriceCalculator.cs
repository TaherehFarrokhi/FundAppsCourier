using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelPriceCalculator : IParcelPriceCalculator
    {
        public ParcelCalculationResult Calculate(Parcel parcel)
        {
            return new ParcelCalculationResult(0m);
        }
    }
}