using Karenderya.Application.Interfaces;
using Karenderya.Domain.Entities;

namespace Karenderya.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _repository;
        public MenuItemService(IMenuItemRepository Repository)
        {
            _repository = Repository;
        }

        public async Task<MenuItem> CreateAsync(MenuItem menuItem)
        {
            return await _repository.CreateAsync(menuItem);
        }

        public async Task<List<MenuItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, MenuItem menuItem)
        {
            return await _repository.UpdateAsync(id, menuItem);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
