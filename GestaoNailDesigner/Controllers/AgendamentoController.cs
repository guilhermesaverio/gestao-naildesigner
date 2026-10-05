
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Models;
using GestaoNailDesigner.Data;

public class AgendamentoController : Controller
{
    private readonly ApplicationDbContext _context;

    public AgendamentoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: AGENDAMENTOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Agendamentos.ToListAsync());
    }

    // GET: AGENDAMENTOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var agendamento = await _context.Agendamentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (agendamento == null)
        {
            return NotFound();
        }

        return View(agendamento);
    }

    // GET: AGENDAMENTOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: AGENDAMENTOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,ClienteId,Cliente,ServicoId,Servico,PacoteClienteId,PacoteCliente,DataHoraInicio,DataHoraFim,Status,StatusPagamento,Observacao,LembreteEnviado")] Agendamento agendamento)
    {
        if (ModelState.IsValid)
        {
            _context.Add(agendamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(agendamento);
    }

    // GET: AGENDAMENTOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var agendamento = await _context.Agendamentos.FindAsync(id);
        if (agendamento == null)
        {
            return NotFound();
        }
        return View(agendamento);
    }

    // POST: AGENDAMENTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,ClienteId,Cliente,ServicoId,Servico,PacoteClienteId,PacoteCliente,DataHoraInicio,DataHoraFim,Status,StatusPagamento,Observacao,LembreteEnviado")] Agendamento agendamento)
    {
        if (id != agendamento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(agendamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendamentoExists(agendamento.Id))
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
        return View(agendamento);
    }

    // GET: AGENDAMENTOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var agendamento = await _context.Agendamentos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (agendamento == null)
        {
            return NotFound();
        }

        return View(agendamento);
    }

    // POST: AGENDAMENTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var agendamento = await _context.Agendamentos.FindAsync(id);
        if (agendamento != null)
        {
            _context.Agendamentos.Remove(agendamento);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AgendamentoExists(int? id)
    {
        return _context.Agendamentos.Any(e => e.Id == id);
    }
}
