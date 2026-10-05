
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Models;
using GestaoNailDesigner.Data;

public class InscricaoCursoController : Controller
{
    private readonly ApplicationDbContext _context;

    public InscricaoCursoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: INSCRICAOCURSOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.InscricoesCursos.ToListAsync());
    }

    // GET: INSCRICAOCURSOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inscricaocurso = await _context.InscricoesCursos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (inscricaocurso == null)
        {
            return NotFound();
        }

        return View(inscricaocurso);
    }

    // GET: INSCRICAOCURSOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: INSCRICAOCURSOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,AlunaId,Aluna,CursoId,Curso,DescontoPercentual,DescontoValor,ValorFinal,DataInicio,DataFim,StatusPagamento,DataInscricao")] InscricaoCurso inscricaocurso)
    {
        if (ModelState.IsValid)
        {
            _context.Add(inscricaocurso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(inscricaocurso);
    }

    // GET: INSCRICAOCURSOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inscricaocurso = await _context.InscricoesCursos.FindAsync(id);
        if (inscricaocurso == null)
        {
            return NotFound();
        }
        return View(inscricaocurso);
    }

    // POST: INSCRICAOCURSOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,AlunaId,Aluna,CursoId,Curso,DescontoPercentual,DescontoValor,ValorFinal,DataInicio,DataFim,StatusPagamento,DataInscricao")] InscricaoCurso inscricaocurso)
    {
        if (id != inscricaocurso.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(inscricaocurso);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscricaoCursoExists(inscricaocurso.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(inscricaocurso);
    }

    // GET: INSCRICAOCURSOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inscricaocurso = await _context.InscricoesCursos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (inscricaocurso == null)
        {
            return NotFound();
        }

        return View(inscricaocurso);
    }

    // POST: INSCRICAOCURSOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var inscricaocurso = await _context.InscricoesCursos.FindAsync(id);
        if (inscricaocurso != null)
        {
            _context.InscricoesCursos.Remove(inscricaocurso);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InscricaoCursoExists(int? id)
    {
        return _context.InscricoesCursos.Any(e => e.Id == id);
    }
}
