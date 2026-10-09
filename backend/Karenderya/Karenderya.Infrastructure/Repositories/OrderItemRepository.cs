using Karenderya.Application.Interfaces;
using Karenderya.Domain.Entities;
using Karenderya.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Karenderya.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;
        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrderItem> CreateAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        //public Task<int> DeleteAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _context.OrderItems.AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        //public Task<int> UpdateAsync(int id, OrderItem orderItem)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
