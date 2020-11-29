using CourierService.Models;

namespace CourierService
{
    public interface IParcelTypeCalculator
    {

        ParcelType GetParcelType(Parcel parcel);
    }
}