using CourierService.Models;

namespace CourierService
{
    public interface IParcelCostProvider
    {
        (ParcelType ParcelType, decimal Cost) ResolveParcelCost(Parcel parcel);
    }
}