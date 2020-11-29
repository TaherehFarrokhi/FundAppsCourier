namespace CourierService
{
    public interface IWeightCostCalculator
    {
        decimal GetCost(decimal weight, decimal wightLimit, decimal overweightCost);
    }
}