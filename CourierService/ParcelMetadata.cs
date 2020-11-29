using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelMetadata
    {
        public ParcelMetadata(ParcelType parcelType, decimal cost, decimal wightLimit)
        {
            ParcelType = parcelType;
            Cost = cost;
            WightLimit = wightLimit;
        }

        public decimal Cost { get; }
        public decimal WightLimit { get; }
        public ParcelType ParcelType { get; }
    }
}