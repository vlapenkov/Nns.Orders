using Microsoft.AspNetCore.Mvc;
using Nns.Orders.Interfaces.Models;
using Nns.Orders.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nns.Orders.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {

        IWorkOrderService _workOrderService;

        public WorkOrderController(IWorkOrderService workOrderService)
        {
            _workOrderService = workOrderService;
        }


        /// <summary>
        /// Получение этапа производственного цикла
        /// </summary>        
        [HttpGet("{id}")]
        public async Task<WorkOrderResponse> Get(int id)
        {
            return await _workOrderService.Get(id);
        }

        /// <summary>
        /// Создание этапа производственного цикла
        /// </summary>        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWorkOrderRequest request)
        {
            var result = await _workOrderService.Add(request);

            return CreatedAtAction(nameof(Get), result);
        }

      
    }
}
