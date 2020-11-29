using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelTypeDefinition
    {
        public decimal Cost { get;  }
        public decimal WightLimit { get; }
        public ParcelType ParcelType { get; }

        public ParcelTypeDefinition(ParcelType parcelType, decimal cost, decimal wightLimit)
        {
            ParcelType = parcelType;
            Cost = cost;
            WightLimit = wightLimit;
        }
    }
}