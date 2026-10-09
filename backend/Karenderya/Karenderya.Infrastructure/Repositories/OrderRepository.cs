using Karenderya.Application.Interfaces;
using Karenderya.Domain.Entities;
using Karenderya.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Karenderya.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        //public Task<int> DeleteAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        //public Task<int> UpdateAsync(int id, Order order)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
