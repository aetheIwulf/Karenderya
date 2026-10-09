using Karenderya.Application.Interfaces;
using Karenderya.Domain.Entities;
using Karenderya.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Karenderya.Infrastructure.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;
        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MenuItem> CreateAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _context.MenuItems
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems.ToListAsync();
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _context.MenuItems.AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<int> UpdateAsync(int id, MenuItem menuItem)
        {
            return await _context.MenuItems
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.Price, menuItem.Price)
                .SetProperty(m => m.AvailableQuantity, menuItem.AvailableQuantity)
                );
        }
    }
}
