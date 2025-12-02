using clienteAPI.Data;
using clienteAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace clienteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>>GetClientes() {
            List<Cliente> ListaClientes = await _context.Clientes.ToListAsync();
            return Ok(ListaClientes);
        }

        [HttpPost]
        public async Task<IActionResult>CreateCliente([FromBody]Cliente cliente) {
            _context.Clientes.Add(cliente);
            int result = await _context.SaveChangesAsync();

            if (result == 1)
            {
                return Ok("Cliente criado com sucesso");
            }
            return BadRequest("Cliente não cadastrado");
        }
    }
}
