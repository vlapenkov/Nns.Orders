using AutoMapper;
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
        private readonly IMapper _mapper;

        public WorkCycleController(IWorkCycleService workOrderService, IMapper mapper)
        {
            _workOrderService = workOrderService;
            _mapper = mapper;
        }


        /// <summary>
        /// Получение этапа производственного цикла
        /// </summary>        
        [HttpGet("{id}")]
        public async Task<WorkCycleResponse> Get(int id)
        {
            var result = await _workOrderService.Get(id);

            return _mapper.Map < WorkCycleResponse>(result);
        }

        /// <summary>
        /// Создание этапа производственного цикла
        /// </summary>        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWorkCycleRequest request)
        {

            var model =_mapper.Map<WorkCycle>(request);

            var result = await _workOrderService.Add(model);

            return CreatedAtAction(nameof(Get), new { Id = result }, result);
        }

      
    }
}
