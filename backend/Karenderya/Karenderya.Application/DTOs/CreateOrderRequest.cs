namespace Karenderya.Application.DTOs
{
    public class CreateOrderRequest
    {
        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }

    public class CreateOrderItemRequest
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}