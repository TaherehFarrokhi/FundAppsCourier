using System;
using System.Linq;
using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelMetadataProvider : IParcelMetadataProvider
    {
        private readonly (Func<decimal, bool> Condition, ParcelMetadata ParcelTypeDefinition)[] _calculationRule =
        {
            (m => m < 10, new ParcelMetadata(ParcelType.Small, 3, 1)),
            (m => m < 50, new ParcelMetadata(ParcelType.Medium, 8, 3)),
            (m => m < 100, new ParcelMetadata(ParcelType.Large, 15, 6)),
            (m => m >= 100, new ParcelMetadata(ParcelType.XLarge, 25, 10))
        };

        public ParcelMetadata ResolveParcelMetadata(Parcel parcel)
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