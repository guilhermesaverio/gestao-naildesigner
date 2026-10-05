
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Models;
using GestaoNailDesigner.Data;

public class PacoteController : Controller
{
    private readonly ApplicationDbContext _context;

    public PacoteController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PACOTES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Pacotes.ToListAsync());
    }

    // GET: PACOTES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pacote = await _context.Pacotes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pacote == null)
        {
            return NotFound();
        }

        return View(pacote);
    }

    // GET: PACOTES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PACOTES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,ServicoId,Servico,QuantidadeSessoes,ValorTotal,Ativo")] Pacote pacote)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pacote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pacote);
    }

    // GET: PACOTES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pacote = await _context.Pacotes.FindAsync(id);
        if (pacote == null)
        {
            return NotFound();
        }
        return View(pacote);
    }

    // POST: PACOTES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,ServicoId,Servico,QuantidadeSessoes,ValorTotal,Ativo")] Pacote pacote)
    {
        if (id != pacote.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pacote);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacoteExists(pacote.Id))
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
        return View(pacote);
    }

    // GET: PACOTES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pacote = await _context.Pacotes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pacote == null)
        {
            return NotFound();
        }

        return View(pacote);
    }

    // POST: PACOTES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var pacote = await _context.Pacotes.FindAsync(id);
        if (pacote != null)
        {
            _context.Pacotes.Remove(pacote);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PacoteExists(int? id)
    {
        return _context.Pacotes.Any(e => e.Id == id);
    }
}
