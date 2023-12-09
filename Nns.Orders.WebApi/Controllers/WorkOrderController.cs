using Microsoft.AspNetCore.Mvc;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces.Logic;
using Nns.Orders.WebApi.Models;
using Nns.Orders.Logic;
using Nns.Orders.WebApi.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using X.PagedList;

namespace Nns.Orders.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        private readonly IWorkOrderService _service;
        private readonly IMapper _mapper;


        public WorkOrderController(IWorkOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Список планов (наряд-заданий)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] WorkOrderFilter filter)
        {
            var pagedList = await _service.GetAll(filter.ExcavationId, filter.WorkTypeId)
                .ToPagedListAsync<WorkOrder>(filter.PageNumber ?? 1, filter.PageSize ?? 50);

            var ordersDto = _mapper.Map<IList<WorkOrderShortResponse>>(pagedList);                       

            return Ok(new
            {
                Total = pagedList.TotalItemCount,
                Items = ordersDto
            });

        }

        /// <summary>
        /// Получение плана (наряд-задания)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<WorkOrderResponse> Get(int id)
        {
            var result = await _service.Get(id);

            return _mapper.Map<WorkOrderResponse>(result);
          
        }

        /// <summary>
        /// Создание плана (наряд-задания)
        /// </summary>  
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWorkOrderRequest request)
        {
            var model =_mapper.Map<WorkOrder>(request);

            var result = await _service.Add(model);

            return CreatedAtAction(nameof(Get), new { Id = result }, result);
        }
       

      
    }
}
