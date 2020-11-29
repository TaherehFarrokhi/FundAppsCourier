namespace CourierService
{
    public sealed class DeliveryOptions
    {
        public bool FastDelivery { get; }

        public DeliveryOptions(bool fastDelivery)
        {
            FastDelivery = fastDelivery;
        }
    }
}