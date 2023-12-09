using Microsoft.AspNetCore.Mvc;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces.Logic;
using Nns.Orders.WebApi.Models;



namespace Nns.Orders.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkCycleController : ControllerBase
    {

        private readonly IWorkCycleService _workOrderService;

        public WorkCycleController(IWorkCycleService workOrderService)
        {
            _workOrderService = workOrderService;
        }


        /// <summary>
        /// Получение этапа производственного цикла
        /// </summary>        
        [HttpGet("{id}")]
        public async Task<WorkCycleResponse> Get(int id)
        {
            var result = await _workOrderService.Get(id);

            return new WorkCycleResponse
            {
                Id = result.Id,
                
                WorkKind = new WorkTypeDto { Id = result.WorkTypeId, Name = result.WorkType.Name },
                IsActive = result.IsActive,
                OrderNumber = result.OrderNumber,
                StartDate = result.StartDate
            };
        }

        /// <summary>
        /// Создание этапа производственного цикла
        /// </summary>        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWorkCycleRequest request)
        {

            var model = new WorkCycle
            {
                StartDate = request.StartDate.ToDateTime(TimeOnly.MinValue),
                WorkTypeId= request.WorkTypeId,                
                IsActive = request.IsActive,
                OrderNumber= request.OrderNumber
            };

            var result = await _workOrderService.Add(model);

            return CreatedAtAction(nameof(Get), new { Id = result }, result);
        }

      
    }
}
