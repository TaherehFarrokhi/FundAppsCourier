namespace CourierService.Models
{
    public sealed class Parcel
    {
        public Parcel(decimal width, decimal length, decimal height)
        {
            Width = width;
            Length = length;
            Height = height;
        }

        public decimal Width { get; }
        public decimal Length { get; }
        public decimal Height { get; }
    }
}