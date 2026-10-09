using Karenderya.Application.Interfaces;
using Karenderya.Domain.Entities;

namespace Karenderya.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _repository;
        public OrderItemService(IOrderItemRepository Repository)
        {
            _repository = Repository;
        }
        public async Task<OrderItem> CreateAsync(OrderItem orderItem)
        {
            return await _repository.CreateAsync(orderItem);
        }

        public async Task<List<OrderItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
