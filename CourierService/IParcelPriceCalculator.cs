using CourierService.Models;

namespace CourierService
{
    public interface IParcelPriceCalculator
    {
        ParcelCalculationResult Calculate(Parcel parcel);
        ParcelCalculationResult Calculate(Parcel parcel, DeliveryOptions deliveryOptions);
    }
}