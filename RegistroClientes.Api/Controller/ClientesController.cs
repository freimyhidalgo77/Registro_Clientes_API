using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clientes.Data.Context;
using Clientes.Data.Models;
using Clientes.Abstraccion;
using Clientes.Domain.DTO;

namespace RegistroClientes.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController(IClientesService clientesService) : ControllerBase
    {
        private readonly ClientesContext _context;

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesDTO>>> GetClientes()
        {
            return await clientesService.Listar(t =>true);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesDTO>> GetCliente(int id)
        {
            return await clientesService.Buscar(id);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClientesDTO clientesDto)
        {
            if (id != clientesDto.ClienteID)
            {
                return BadRequest();
            }

            // Actualizar el cliente
            await clientesService.Guardar(clientesDto);
            return NoContent();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClientesDTO clientesDTO)
        {
            await clientesService.Guardar(clientesDTO);
            return CreatedAtAction("GetClientes", new { id = clientesDTO.ClienteID }, clientesDTO );

        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await clientesService.Eliminar(id);
            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
