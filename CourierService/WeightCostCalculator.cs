using System;

namespace CourierService
{
    public sealed class WeightCostCalculator : IWeightCostCalculator
    {
        public decimal GetCost(decimal weight, decimal wightLimit, decimal overweightCost)
        {
            return weight > wightLimit
                ? Math.Ceiling(weight - wightLimit) * overweightCost
                : 0;
        }
    }
}