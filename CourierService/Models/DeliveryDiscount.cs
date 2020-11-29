namespace CourierService.Models
{
    public sealed class DeliveryDiscount
    {
        public DeliveryDiscount(string description, decimal discount)
        {
            Description = description;
            Discount = discount;
        }

        public string Description { get; }
        public decimal Discount { get; }
    }
}