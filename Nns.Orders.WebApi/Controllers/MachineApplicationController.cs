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

        // GET: api/<MachineApplicationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MachineApplicationController>/5
        [HttpGet("{id}")]
        public async Task<MachineApplicationResponse> Get(int id)
        {
            return await _service.Get(id);
        }

        // POST api/<MachineApplicationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMachineApplicationRequest request)
        {
          var result =await _service.Add(request);

            return CreatedAtAction(nameof(Get),result);
        }

        // PUT api/<MachineApplicationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MachineApplicationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
