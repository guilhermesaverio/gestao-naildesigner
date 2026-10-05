using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Models;
using GestaoNailDesigner.Data;

namespace GestaoNailDesigner.Controllers
{
    public class InscricaoCursoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InscricaoCursoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InscricaoCurso
        public async Task<IActionResult> Index()
        {
            var inscricoes = _context.InscricoesCursos
                .Include(i => i.Aluna)
                .Include(i => i.Curso);

            return View(await inscricoes.ToListAsync());
        }

        // GET: InscricaoCurso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var inscricaocurso = await _context.InscricoesCursos
                .Include(i => i.Aluna)
                .Include(i => i.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (inscricaocurso == null) return NotFound();

            return View(inscricaocurso);
        }

        // GET: InscricaoCurso/Create
        public IActionResult Create()
        {
            CarregarViewBags(); 
            return View();
        }

        // POST: InscricaoCurso/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AlunaId,CursoId,DescontoPercentual,DescontoValor,ValorFinal,DataInicio,DataFim,StatusPagamento,DataInscricao")] InscricaoCurso inscricaocurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscricaocurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            CarregarViewBags(inscricaocurso);
            return View(inscricaocurso);
        }

        // GET: InscricaoCurso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var inscricaocurso = await _context.InscricoesCursos.FindAsync(id);
            if (inscricaocurso == null) return NotFound();

            CarregarViewBags(inscricaocurso);
            return View(inscricaocurso);
        }

        // POST: InscricaoCurso/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,AlunaId,CursoId,DescontoPercentual,DescontoValor,ValorFinal,DataInicio,DataFim,StatusPagamento,DataInscricao")] InscricaoCurso inscricaocurso)
        {
            if (id != inscricaocurso.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscricaocurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscricaoCursoExists(inscricaocurso.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            CarregarViewBags(inscricaocurso);
            return View(inscricaocurso);
        }

        // GET: InscricaoCurso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var inscricaocurso = await _context.InscricoesCursos
                .Include(i => i.Aluna)
                .Include(i => i.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (inscricaocurso == null) return NotFound();

            return View(inscricaocurso);
        }

        // POST: InscricaoCurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var inscricaocurso = await _context.InscricoesCursos.FindAsync(id);
            if (inscricaocurso != null)
            {
                _context.InscricoesCursos.Remove(inscricaocurso);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool InscricaoCursoExists(int? id)
        {
            return _context.InscricoesCursos.Any(e => e.Id == id);
        }

        private void CarregarViewBags(InscricaoCurso inscricao = null)
        {
            ViewData["AlunaId"] = new SelectList(_context.Alunas, "Id", "Nome", inscricao?.AlunaId);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome", inscricao?.CursoId);
        }
    }
}