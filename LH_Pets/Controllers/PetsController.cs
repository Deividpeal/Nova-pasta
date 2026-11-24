
using Microsoft.AspNetCore.Mvc;
using LH_Pets.Models;

namespace LH_Pets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        // GET: api/pets
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(DadosEmMemoria.Pets);
        }

        // POST: api/pets
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Pet pet)
        {
            // Verifica se o cliente informado existe
            var clienteExiste = DadosEmMemoria.Clientes
                .Any(c => c.Id == pet.ClienteId);

            if (!clienteExiste)
            {
                return BadRequest("Cliente não encontrado.");
            }

            // Gera o ID automaticamente
            pet.Id = DadosEmMemoria.Pets.Count == 0
                ? 1
                : DadosEmMemoria.Pets.Max(p => p.Id) + 1;

            DadosEmMemoria.Pets.Add(pet);

            return Created("/api/pets", pet);
        }

        // GET: api/pets/cliente/{clienteId}
        [HttpGet("cliente/{clienteId}")]
        public IActionResult ObterPorCliente(int clienteId)
        {
            var clienteExiste = DadosEmMemoria.Clientes
                .Any(c => c.Id == clienteId);

            if (!clienteExiste)
            {
                return NotFound("Cliente não encontrado.");
            }

            var petsDoCliente = DadosEmMemoria.Pets
                .Where(p => p.ClienteId == clienteId)
                .ToList();

            return Ok(petsDoCliente);
        }
    }
}
