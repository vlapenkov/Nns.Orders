using Microsoft.AspNetCore.Mvc;
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

        // GET: api/<OrderPlanController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderPlanController>/5
        [HttpGet("{id}")]
        public async Task<OrderPlanResponse> Get(int id)
        {
            return await _service.Get(id);
        }

        // POST api/<OrderPlanController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderPlanRequest request)
        {
            var result = await _service.Add(request);

            return CreatedAtAction(nameof(Get), result);
        }

        // PUT api/<OrderPlanController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderPlanController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
