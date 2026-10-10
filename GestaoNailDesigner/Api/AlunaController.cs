using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Data;
using GestaoNailDesigner.Models;

namespace GestaoNailDesigner.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlunaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Aluna
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluna>>> GetAlunas()
        {
            return await _context.Alunas.ToListAsync();
        }

        // GET: api/Aluna/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluna>> GetAluna(int id)
        {
            var aluna = await _context.Alunas.FindAsync(id);

            if (aluna == null)
            {
                return NotFound();
            }

            return aluna;
        }

        // POST: api/Aluna
        [HttpPost]
        public async Task<ActionResult<Aluna>> PostAluna(Aluna aluna)
        {
            _context.Alunas.Add(aluna);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluna), new { id = aluna.Id }, aluna);
        }
    }
}