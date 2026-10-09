using Karenderya.Domain.Entities;

namespace Karenderya.Application.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAllAsync();
        Task<OrderItem> GetByIdAsync(int id);
        Task<OrderItem> CreateAsync(OrderItem orderItem);
        //Task<int> UpdateAsync(int id, OrderItem orderItem);
        //Task<int> DeleteAsync(int id);
    }
}
