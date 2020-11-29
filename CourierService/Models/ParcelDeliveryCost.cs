namespace CourierService.Models
{
    public sealed class ParcelDeliveryCost
    {
        public ParcelDeliveryCost(string parcelName, ParcelType parcelType, decimal cost)
        {
            ParcelName = parcelName;
            ParcelType = parcelType;
            Cost = cost;
        }

        public string ParcelName { get; }
        public ParcelType ParcelType { get; }
        public decimal Cost { get; }
    }
}