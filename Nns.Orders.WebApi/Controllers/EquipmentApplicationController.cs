using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Logic;
using Nns.Orders.WebApi.Models;
using Nns.Orders.Logic;
using Nns.Orders.WebApi.Models;



namespace Nns.Orders.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentApplicationController : ControllerBase
    {
        private IEquipmentApplicationService _service;

        public EquipmentApplicationController(IEquipmentApplicationService service)
        {
            _service = service;
        }

       
        /// <summary>
        /// Получение применяемости по выработке
        /// </summary>
        [HttpGet("{id:long}")]
        public async Task<EquipmentApplicationResponse> Get(long id)
        {
            var result = await _service.Get(id);

            return new EquipmentApplicationResponse
            {
                Id = result.Id,
                EquipmentType = new EquipmentTypeDto { Id = result.Id, Name = result.EquipmentType.Name },
                WorkType = new WorkTypeDto { Id = result.WorkTypeId, Name = result.WorkType.Name },
                IsActive = result.IsActive
            };
        }

        /// <summary>
        /// Создание применяемости по выработке
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateEquipmentApplicationRequest request)
        {

            EquipmentApplication model = new()
            {
                StartDate = request.StartDate.ToDateTime(TimeOnly.MinValue),
                EquipmentTypeId = request.EquipmentTypeId,
                WorkTypeId = request.WorkTypeId,
                IsActive = request.IsActive
            };

            var result =await _service.Add(model);

            return CreatedAtAction(nameof(Get),new {Id = result }, result);
            
            
        }
       

        
    }
}
