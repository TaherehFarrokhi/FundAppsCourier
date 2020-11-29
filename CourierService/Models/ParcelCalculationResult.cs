namespace CourierService.Models
{
    public sealed class ParcelCalculationResult
    {
        public ParcelCalculationResult(decimal totalCost)
        {
            TotalCost = totalCost;
        }

        public decimal TotalCost { get; }
    }
}