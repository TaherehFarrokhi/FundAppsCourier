using System.Linq;

namespace CourierService.Models
{
    public sealed class Parcel
    {
        public Parcel(decimal width, decimal length, decimal height, decimal weight)
        {
            Width = width;
            Length = length;
            Height = height;
            Weight = weight;
        }

        public decimal Width { get; }
        public decimal Length { get; }
        public decimal Height { get; }
        public decimal Weight { get; }
    }

    public static class ParcelExtensions
    {
        public static decimal MaxDimension(this Parcel parcel)
        {
            return new[] {parcel.Width, parcel.Length, parcel.Height}.Max();
        }
    }
}