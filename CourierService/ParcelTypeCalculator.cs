using System.Linq;
using CourierService.Models;

namespace CourierService
{
    public sealed class ParcelTypeCalculator : IParcelTypeCalculator
    {
        public ParcelType GetParcelType(Parcel parcel)
        {
            var dimensions = new decimal[]{parcel.Width, parcel.Length, parcel.Height};
            var maxDimension = dimensions.Max();
            
            if (maxDimension < 10)
                return ParcelType.Small;

            if (maxDimension < 50)
                return ParcelType.Medium;
            return maxDimension < 100 ? ParcelType.Large : ParcelType.XLarge;
        }
    }
}