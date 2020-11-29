namespace CourierService.Models
{
    public sealed class ParcelCalculationResult
    {
        public ParcelCalculationResult(decimal deliveryCost, decimal fastDeliveryCost)
        {
            DeliveryCost = deliveryCost;
            FastDeliveryCost = fastDeliveryCost;
        }

        public decimal DeliveryCost { get; }
        public decimal FastDeliveryCost { get; }
        public decimal TotalCost => DeliveryCost + FastDeliveryCost;
    }
}