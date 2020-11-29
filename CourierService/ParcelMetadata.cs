using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelMetadata
    {
        public ParcelMetadata(ParcelType parcelType, decimal cost, decimal wightLimit, decimal overweightCost)
        {
            ParcelType = parcelType;
            Cost = cost;
            WightLimit = wightLimit;
            OverweightCost = overweightCost;
        }

        public decimal Cost { get; }
        public decimal WightLimit { get; }
        public ParcelType ParcelType { get; }
        public decimal OverweightCost { get; }
    }
}