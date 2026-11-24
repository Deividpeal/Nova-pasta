
using Microsoft.AspNetCore.Mvc;
using LH_Pets.Models;

namespace LH_Pets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        // GET: api/clientes
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(DadosEmMemoria.Clientes);
        }

        // POST: api/clientes
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Cliente cliente)
        {
            cliente.Id = DadosEmMemoria.Clientes.Count == 0
                ? 1
                : DadosEmMemoria.Clientes.Max(c => c.Id) + 1;

            DadosEmMemoria.Clientes.Add(cliente);

            return Created("/api/clientes", cliente);
        }
    }
}
