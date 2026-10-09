using Karenderya.Application.Interfaces;
using Karenderya.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Karenderya.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService _service;
        public OrderItemController(IOrderItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _service.GetByIdAsync(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItem orderItem)
        {
            var result = await _service.CreateAsync(orderItem);
            return Ok(result);
        }
    }
}
