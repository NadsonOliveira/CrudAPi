using CrudAPi.Models;
using CrudAPi.Services.Cliente;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteInterface _clienteService;

        public ClienteController(IClienteInterface clienteService)
        {
            _clienteService = clienteService;
        }


        // GET: api/cliente
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ClienteModel>>>> ListarClientes()
        {
            var response = await _clienteService.ListarClientes();
            return Ok(response);
        }

        // GET: api/cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> BuscarClientePorId(int id)
        {
            var response = await _clienteService.BuscarClientePorId(id);
            if (!response.Status)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/cliente
        [HttpPost]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> CriarCliente([FromBody] ClienteModel cliente)
        {
            var response = await _clienteService.CriarCliente(cliente);
            return CreatedAtAction(nameof(BuscarClientePorId), new { id = response.Dados?.Id }, response);
        }

        // PUT: api/cliente/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> AtualizarCliente(int id, [FromBody] ClienteModel cliente)
        {
            var response = await _clienteService.AtualizarCliente(id, cliente);
            if (!response.Status)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // DELETE: api/cliente/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> DeletarCliente(int id)
        {
            var response = await _clienteService.DeletarCliente(id);
            if (!response.Status)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
