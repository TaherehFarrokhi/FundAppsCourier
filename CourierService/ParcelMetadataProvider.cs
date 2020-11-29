using System;
using System.Linq;
using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelMetadataProvider : IParcelMetadataProvider
    {
        private const decimal DefaultOverweightCost = 2;

        private readonly (Func<Parcel, bool> Condition, ParcelMetadata ParcelTypeDefinition)[] _calculationPriceRule =
        {
            (m => m.Weight >= 50, new ParcelMetadata(ParcelType.Heavy, 50, 50, 1)),
            (m => m.MaxDimension() < 10, new ParcelMetadata(ParcelType.Small, 3, 1, DefaultOverweightCost)),
            (m => m.MaxDimension() < 50, new ParcelMetadata(ParcelType.Medium, 8, 3, DefaultOverweightCost)),
            (m => m.MaxDimension() < 100, new ParcelMetadata(ParcelType.Large, 15, 6, DefaultOverweightCost)),
            (m => m.MaxDimension() >= 100, new ParcelMetadata(ParcelType.XLarge, 25, 10, DefaultOverweightCost))
        };

        public ParcelMetadata ResolveParcelMetadata(Parcel parcel)
        {
            return _calculationPriceRule
                .Where(cr => cr.Condition(parcel))
                .Select(cr => cr.ParcelTypeDefinition)
                .FirstOrDefault();
        }
    }
}