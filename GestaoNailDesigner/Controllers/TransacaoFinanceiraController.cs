
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Models;
using GestaoNailDesigner.Data;

public class TransacaoFinanceiraController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransacaoFinanceiraController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: TRANSACAOFINANCEIRAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.TransacoesFinanceiras.ToListAsync());
    }

    // GET: TRANSACAOFINANCEIRAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transacaofinanceira = await _context.TransacoesFinanceiras
            .FirstOrDefaultAsync(m => m.Id == id);
        if (transacaofinanceira == null)
        {
            return NotFound();
        }

        return View(transacaofinanceira);
    }

    // GET: TRANSACAOFINANCEIRAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TRANSACAOFINANCEIRAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Descricao,Valor,CategoriaId,Categoria,Data,FormaPagamento,AgendamentoId,Agendamento,PacoteClienteId,PacoteCliente")] TransacaoFinanceira transacaofinanceira)
    {
        if (ModelState.IsValid)
        {
            _context.Add(transacaofinanceira);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(transacaofinanceira);
    }

    // GET: TRANSACAOFINANCEIRAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transacaofinanceira = await _context.TransacoesFinanceiras.FindAsync(id);
        if (transacaofinanceira == null)
        {
            return NotFound();
        }
        return View(transacaofinanceira);
    }

    // POST: TRANSACAOFINANCEIRAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Descricao,Valor,CategoriaId,Categoria,Data,FormaPagamento,AgendamentoId,Agendamento,PacoteClienteId,PacoteCliente")] TransacaoFinanceira transacaofinanceira)
    {
        if (id != transacaofinanceira.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(transacaofinanceira);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransacaoFinanceiraExists(transacaofinanceira.Id))
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
        return View(transacaofinanceira);
    }

    // GET: TRANSACAOFINANCEIRAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transacaofinanceira = await _context.TransacoesFinanceiras
            .FirstOrDefaultAsync(m => m.Id == id);
        if (transacaofinanceira == null)
        {
            return NotFound();
        }

        return View(transacaofinanceira);
    }

    // POST: TRANSACAOFINANCEIRAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var transacaofinanceira = await _context.TransacoesFinanceiras.FindAsync(id);
        if (transacaofinanceira != null)
        {
            _context.TransacoesFinanceiras.Remove(transacaofinanceira);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TransacaoFinanceiraExists(int? id)
    {
        return _context.TransacoesFinanceiras.Any(e => e.Id == id);
    }
}
