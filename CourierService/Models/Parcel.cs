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
}