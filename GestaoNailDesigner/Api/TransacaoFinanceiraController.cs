using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Data;
using GestaoNailDesigner.Models;

namespace GestaoNailDesigner.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoFinanceiraController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransacaoFinanceiraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TransacaoFinanceira
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransacaoFinanceira>>> GetTransacoesFinanceiras()
        {
            return await _context.TransacoesFinanceiras
                .Include(t => t.Categoria)
                .ToListAsync();
        }

        // GET: api/TransacaoFinanceira/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransacaoFinanceira>> GetTransacaoFinanceira(int id)
        {
            var transacaoFinanceira = await _context.TransacoesFinanceiras
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transacaoFinanceira == null)
            {
                return NotFound();
            }

            return transacaoFinanceira;
        }

        // POST: api/TransacaoFinanceira
        [HttpPost]
        public async Task<ActionResult<TransacaoFinanceira>> PostTransacaoFinanceira(TransacaoFinanceira transacaoFinanceira)
        {
            _context.TransacoesFinanceiras.Add(transacaoFinanceira);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransacaoFinanceira), new { id = transacaoFinanceira.Id }, transacaoFinanceira);
        }
    }
}