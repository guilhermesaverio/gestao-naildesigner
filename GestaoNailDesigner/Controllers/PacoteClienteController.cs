
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Models;
using GestaoNailDesigner.Data;

public class PacoteClienteController : Controller
{
    private readonly ApplicationDbContext _context;

    public PacoteClienteController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PACOTECLIENTES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.PacotesClientes.ToListAsync());
    }

    // GET: PACOTECLIENTES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pacotecliente = await _context.PacotesClientes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pacotecliente == null)
        {
            return NotFound();
        }

        return View(pacotecliente);
    }

    // GET: PACOTECLIENTES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PACOTECLIENTES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,ClienteId,Cliente,PacoteId,Pacote,SessoesRestantes,DataCompra,Status")] PacoteCliente pacotecliente)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pacotecliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pacotecliente);
    }

    // GET: PACOTECLIENTES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pacotecliente = await _context.PacotesClientes.FindAsync(id);
        if (pacotecliente == null)
        {
            return NotFound();
        }
        return View(pacotecliente);
    }

    // POST: PACOTECLIENTES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,ClienteId,Cliente,PacoteId,Pacote,SessoesRestantes,DataCompra,Status")] PacoteCliente pacotecliente)
    {
        if (id != pacotecliente.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pacotecliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacoteClienteExists(pacotecliente.Id))
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
        return View(pacotecliente);
    }

    // GET: PACOTECLIENTES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pacotecliente = await _context.PacotesClientes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pacotecliente == null)
        {
            return NotFound();
        }

        return View(pacotecliente);
    }

    // POST: PACOTECLIENTES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var pacotecliente = await _context.PacotesClientes.FindAsync(id);
        if (pacotecliente != null)
        {
            _context.PacotesClientes.Remove(pacotecliente);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PacoteClienteExists(int? id)
    {
        return _context.PacotesClientes.Any(e => e.Id == id);
    }
}
