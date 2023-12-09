using Microsoft.AspNetCore.Mvc;
using Nns.Orders.Common.PagedList;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces.Logic;
using Nns.Orders.WebApi.Models;
using Nns.Orders.Logic;
using Nns.Orders.WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nns.Orders.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        private readonly IWorkOrderService _service;

        public WorkOrderController(IWorkOrderService service)
        {
            _service = service;
        }

        /// <summary>
        /// Список планов (наряд-заданий)
        /// </summary>
        [HttpGet]
        public async Task<PagedList<WorkOrderResponse>> Get([FromQuery]WorkOrderFilter filter)
        {
            //return await _service.Get(filter);
            throw new NotImplementedException();    
        }

        /// <summary>
        /// Получение плана (наряд-задания)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<WorkOrderResponse> Get(int id)
        {
            var result = await _service.Get(id);

            return new WorkOrderResponse
            {
                Id = result.Id,
                IsComplete = result.IsComplete,
                EquipmentType = new EquipmentTypeDto { Id = result.EquipmentTypeId, Name = result.EquipmentType.Name },
                WorkType = new WorkTypeDto { Id = result.WorkTypeId, Name = result.WorkType.Name },
                Excavation = new ExcavationDto { Id = result.ExcavationId, Name = result.Excavation.Name },
                OrderNumber = result.OrderNumber,
                Value = result.Value
            };
        }

        /// <summary>
        /// Создание плана (наряд-задания)
        /// </summary>  
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWorkOrderRequest request)
        {
            WorkOrder model = new()
            {
                StartDate = request.StartDate.ToDateTime(TimeOnly.MinValue),
                ExcavationId = request.ExcavationId,
                WorkTypeId = request.WorkTypeId,
                EquipmentTypeId = request.EquipmentTypeId,
                Value = request.Value,
                OrderNumber = request.OrderNumber,
                IsComplete = request.IsComplete
            };

            var result = await _service.Add(model);

            return CreatedAtAction(nameof(Get), new { Id = result }, result);
        }
       

      
    }
}
