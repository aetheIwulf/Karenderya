using Karenderya.Domain.Entities;

namespace Karenderya.Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<List<MenuItem>> GetAllAsync();
        Task<MenuItem> GetByIdAsync(int id);
        Task<MenuItem> CreateAsync(MenuItem menuItem);
        Task<int> UpdateAsync(int id, MenuItem menuItem);
        Task<int> DeleteAsync(int id);
    }
}
