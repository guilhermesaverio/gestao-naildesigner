
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Models;
using GestaoNailDesigner.Data;

public class ServicoController : Controller
{
    private readonly ApplicationDbContext _context;

    public ServicoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: SERVICOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Servicos.ToListAsync());
    }

    // GET: SERVICOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var servico = await _context.Servicos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (servico == null)
        {
            return NotFound();
        }

        return View(servico);
    }

    // GET: SERVICOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SERVICOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Preco,DuracaoMinutos,Ativo")] Servico servico)
    {
        if (ModelState.IsValid)
        {
            _context.Add(servico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(servico);
    }

    // GET: SERVICOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var servico = await _context.Servicos.FindAsync(id);
        if (servico == null)
        {
            return NotFound();
        }
        return View(servico);
    }

    // POST: SERVICOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Preco,DuracaoMinutos,Ativo")] Servico servico)
    {
        if (id != servico.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(servico);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicoExists(servico.Id))
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
        return View(servico);
    }

    // GET: SERVICOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var servico = await _context.Servicos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (servico == null)
        {
            return NotFound();
        }

        return View(servico);
    }

    // POST: SERVICOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var servico = await _context.Servicos.FindAsync(id);
        if (servico != null)
        {
            _context.Servicos.Remove(servico);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ServicoExists(int? id)
    {
        return _context.Servicos.Any(e => e.Id == id);
    }
}
