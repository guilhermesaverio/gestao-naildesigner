using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Data;
using GestaoNailDesigner.Models;

namespace GestaoNailDesigner.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AgendamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Agendamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agendamento>>> GetAgendamentos()
        {
            return await _context.Agendamentos.ToListAsync();
        }

        // GET: api/Agendamento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return agendamento;
        }

        // POST: api/Agendamento
        [HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgendamento(Agendamento agendamento)
        {
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAgendamento), new { id = agendamento.Id }, agendamento);
        }
    }
}