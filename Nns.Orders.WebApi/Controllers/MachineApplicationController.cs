using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Models;
using Nns.Orders.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nns.Orders.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineApplicationController : ControllerBase
    {
        MachineApplicationService _service;

        public MachineApplicationController(MachineApplicationService service)
        {
            _service = service;
        }

       
        /// <summary>
        /// Получение применяемости по выработке
        /// </summary>
        [HttpGet("{id}")]
        public async Task<MachineApplicationResponse> Get(int id)
        {
            return await _service.Get(id);
        }

        /// <summary>
        /// Создание применяемости по выработке
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMachineApplicationRequest request)
        {
          var result =await _service.Add(request);

            return CreatedAtAction(nameof(Get),result);
        }
       

        
    }
}
