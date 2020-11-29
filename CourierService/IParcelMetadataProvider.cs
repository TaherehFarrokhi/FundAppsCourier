using CourierService.Models;

namespace CourierService
{
    public interface IParcelMetadataProvider
    {
        ParcelMetadata ResolveParcelMetadata(Parcel parcel);
    }
}