using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Data;
using GestaoNailDesigner.Models;

namespace GestaoNailDesigner.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscricaoCursoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InscricaoCursoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InscricaoCurso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscricaoCurso>>> GetInscricoesCursos()
        {
            return await _context.InscricoesCursos
                .Include(i => i.Aluna)
                .Include(i => i.Curso)
                .ToListAsync();
        }

        // GET: api/InscricaoCurso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InscricaoCurso>> GetInscricaoCurso(int id)
        {
            var inscricaoCurso = await _context.InscricoesCursos
                .Include(i => i.Aluna)
                .Include(i => i.Curso)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inscricaoCurso == null)
            {
                return NotFound();
            }

            return inscricaoCurso;
        }

        // POST: api/InscricaoCurso
        [HttpPost]
        public async Task<ActionResult<InscricaoCurso>> PostInscricaoCurso(InscricaoCurso inscricaoCurso)
        {
            _context.InscricoesCursos.Add(inscricaoCurso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInscricaoCurso), new { id = inscricaoCurso.Id }, inscricaoCurso);
        }
    }
}