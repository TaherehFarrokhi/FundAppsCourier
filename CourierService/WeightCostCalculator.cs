using System;

namespace CourierService
{
    public sealed class WeightCostCalculator : IWeightCostCalculator
    {
        public decimal GetCost(decimal weight, decimal wightLimit)
        {
            return weight > wightLimit
                ? Math.Ceiling(weight - wightLimit) * 2
                : 0;
        }
    }
}