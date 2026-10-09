using Karenderya.Application.Interfaces;
using Karenderya.Domain.Entities;

namespace Karenderya.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository Repository)
        {
            _repository = Repository;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            return await _repository.CreateAsync(order);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
