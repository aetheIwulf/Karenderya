using Karenderya.Application.DTOs;
using Karenderya.Domain.Entities;

namespace Karenderya.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        //Task<int> UpdateAsync(int id, Order order);
        //Task<int> DeleteAsync(int id);
    }
}
