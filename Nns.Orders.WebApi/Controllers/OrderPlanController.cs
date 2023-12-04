using Microsoft.AspNetCore.Mvc;
using Nns.Orders.Common.PagedList;
using Nns.Orders.Interfaces.Logic;
using Nns.Orders.Interfaces.Models;
using Nns.Orders.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nns.Orders.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPlanController : ControllerBase
    {
        private readonly IOrderPlanService _service;

        public OrderPlanController(IOrderPlanService service)
        {
            _service = service;
        }

        /// <summary>
        /// Список планов (наряд-заданий)
        /// </summary>
        [HttpGet]
        public async Task<PagedList<OrderPlanResponse>> Get([FromQuery]OrderPlanFilter filter)
        {
            return await _service.Get(filter);
        }

        /// <summary>
        /// Получение плана (наряд-задания)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<OrderPlanResponse> Get(int id)
        {
            return await _service.Get(id);
        }

        /// <summary>
        /// Создание плана (наряд-задания)
        /// </summary>  
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderPlanRequest request)
        {
            var result = await _service.Add(request);

            return CreatedAtAction(nameof(Get), new { Id = result }, request);
        }
       

      
    }
}
