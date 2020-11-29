using System;
using System.Linq;
using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelCostProvider : IParcelCostProvider
    {
        private readonly (Func<decimal, bool> Condition, ParcelTypeDefinition ParcelTypeDefinition)[] _calculationRule =
        {
            (m => m < 10, new ParcelTypeDefinition(ParcelType.Small, 3, 1)),
            (m => m < 50, new ParcelTypeDefinition(ParcelType.Medium, 8, 3)),
            (m => m < 100, new ParcelTypeDefinition(ParcelType.Large, 15, 6)),
            (m => m >= 100, new ParcelTypeDefinition(ParcelType.XLarge, 25,10))
        };

        public ParcelTypeDefinition ResolveParcelCost(Parcel parcel)
        {
            var dimensions = new[] {parcel.Width, parcel.Length, parcel.Height};
            var maxDimension = dimensions.Max();

            return _calculationRule
                .Where(cr => cr.Condition(maxDimension))
                .Select(cr => cr.ParcelTypeDefinition)
                .FirstOrDefault();
        }
    }
}