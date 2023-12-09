using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Logic;
using Nns.Orders.WebApi.Models;
using Nns.Orders.Logic;
using Nns.Orders.WebApi.Models;
using AutoMapper;

namespace Nns.Orders.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Equipment2WorkController : ControllerBase
    {
        private readonly IEquipmentApplicationService _service;
        private readonly IMapper _mapper;

        public Equipment2WorkController(IEquipmentApplicationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;   
        }

       
        /// <summary>
        /// Получение применяемости по выработке
        /// </summary>
        [HttpGet("{id:long}")]
        public async Task<Equipment2WorkResponse> Get(long id)
        {
            var result = await _service.Get(id);

            return _mapper.Map<Equipment2WorkResponse>(result);
        }

        /// <summary>
        /// Создание применяемости по выработке
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateEquipment2WorkRequest request)
        {

           var model =  _mapper.Map<EquipmentApplication>(request);

            var result =await _service.Add(model);

            return CreatedAtAction(nameof(Get),new {Id = result }, result);
            
            
        }
       

        
    }
}
