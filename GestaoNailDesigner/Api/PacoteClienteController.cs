using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Data;
using GestaoNailDesigner.Models;

namespace GestaoNailDesigner.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacoteClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PacoteClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PacoteCliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacoteCliente>>> GetPacotesClientes()
        {
            return await _context.PacotesClientes
                .Include(p => p.Cliente) // <-- Alterado de p.Aluna para p.Cliente
                .ToListAsync();
        }

        // GET: api/PacoteCliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PacoteCliente>> GetPacoteCliente(int id)
        {
            var pacoteCliente = await _context.PacotesClientes
                .Include(p => p.Cliente) // <-- Alterado de p.Aluna para p.Cliente
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pacoteCliente == null)
            {
                return NotFound();
            }

            return pacoteCliente;
        }

        // POST: api/PacoteCliente
        [HttpPost]
        public async Task<ActionResult<PacoteCliente>> PostPacoteCliente(PacoteCliente pacoteCliente)
        {
            _context.PacotesClientes.Add(pacoteCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPacoteCliente), new { id = pacoteCliente.Id }, pacoteCliente);
        }
    }
}