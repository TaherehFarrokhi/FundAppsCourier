using System;
using System.Linq;
using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelCostProvider : IParcelCostProvider
    {
        private readonly (Func<decimal, bool> Predicate, ParcelType ParcelType, decimal Cost)[] _calculationRule =
        {
            (m => m < 10, ParcelType.Small, 3),
            (m => m < 50, ParcelType.Medium, 8),
            (m => m < 100, ParcelType.Large, 15),
            (m => m >= 100, ParcelType.XLarge, 25)
        };

        public (ParcelType ParcelType, decimal Cost) ResolveParcelCost(Parcel parcel)
        {
            var dimensions = new[] {parcel.Width, parcel.Length, parcel.Height};
            var maxDimension = dimensions.Max();

            return _calculationRule
                .Where(cr => cr.Predicate(maxDimension))
                .Select(cr => (cr.ParcelType, cr.Cost))
                .FirstOrDefault();
        }
    }
}