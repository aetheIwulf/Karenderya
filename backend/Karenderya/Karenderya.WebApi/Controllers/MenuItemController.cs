using Karenderya.Application.Interfaces;
using Karenderya.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Karenderya.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IMenuItemService _service;
        public MenuItemController (IMenuItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("(id)")]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _service.GetByIdAsync(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuItem menuItem)
        {
            var result = await _service.CreateAsync(menuItem);
            return Ok(result);
        }

        [HttpPut("(id)")]
        public async Task<IActionResult> Update(int Id, MenuItem menuItem)
        {
            int existing = await _service.UpdateAsync(Id, menuItem);
            if (existing == 0)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("(id)")]
        public async Task<IActionResult> Delete(int Id)
        {
            int existing = await _service.DeleteAsync(Id);
            if (existing == 0)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
