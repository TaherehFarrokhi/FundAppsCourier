namespace CourierService
{
    public sealed class DeliveryOptions
    {
        public DeliveryOptions(bool fastDelivery)
        {
            FastDelivery = fastDelivery;
        }

        public bool FastDelivery { get; }
    }
}