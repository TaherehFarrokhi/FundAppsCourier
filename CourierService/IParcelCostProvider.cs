using CourierService.Models;

namespace CourierService
{
    public interface IParcelCostProvider
    {
        ParcelTypeDefinition ResolveParcelCost(Parcel parcel);
    }
}