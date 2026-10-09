namespace Karenderya.Domain.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }
    }
}
