namespace Karenderya.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<OrderItem> Items { get; set; }
            = new List<OrderItem>();
    }
}
