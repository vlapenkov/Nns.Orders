using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Logic;
using Nns.Orders.Interfaces.Models;
using Nns.Orders.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nns.Orders.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineApplicationController : ControllerBase
    {
        private IMachineApplicationService _service;

        public MachineApplicationController(IMachineApplicationService service)
        {
            _service = service;
        }

       
        /// <summary>
        /// Получение применяемости по выработке
        /// </summary>
        [HttpGet("{id:long}")]
        public async Task<MachineApplicationResponse> Get(long id)
        {
            return await _service.Get(id);
        }

        /// <summary>
        /// Создание применяемости по выработке
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateMachineApplicationRequest request)
        {
          var result =await _service.Add(request);

            return CreatedAtAction(nameof(Get),new {Id = result }, request);
            
            
        }
       

        
    }
}
