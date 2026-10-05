
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Models;
using GestaoNailDesigner.Data;

public class AlunaController : Controller
{
    private readonly ApplicationDbContext _context;

    public AlunaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ALUNAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Alunas.ToListAsync());
    }

    // GET: ALUNAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var aluna = await _context.Alunas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (aluna == null)
        {
            return NotFound();
        }

        return View(aluna);
    }

    // GET: ALUNAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ALUNAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,Email,DataCadastro")] Aluna aluna)
    {
        if (ModelState.IsValid)
        {
            _context.Add(aluna);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(aluna);
    }

    // GET: ALUNAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var aluna = await _context.Alunas.FindAsync(id);
        if (aluna == null)
        {
            return NotFound();
        }
        return View(aluna);
    }

    // POST: ALUNAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Telefone,Email,DataCadastro")] Aluna aluna)
    {
        if (id != aluna.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(aluna);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunaExists(aluna.Id))
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
        return View(aluna);
    }

    // GET: ALUNAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var aluna = await _context.Alunas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (aluna == null)
        {
            return NotFound();
        }

        return View(aluna);
    }

    // POST: ALUNAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var aluna = await _context.Alunas.FindAsync(id);
        if (aluna != null)
        {
            _context.Alunas.Remove(aluna);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AlunaExists(int? id)
    {
        return _context.Alunas.Any(e => e.Id == id);
    }
}
