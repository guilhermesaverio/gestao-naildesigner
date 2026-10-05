using GestaoNailDesigner.Data;
using GestaoNailDesigner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestaoNailDesigner.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgendamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agendamentos
        public async Task<IActionResult> Index()
        {
            
            var agendamentos = _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Servico)
                .Include(a => a.PacoteCliente)
                .ThenInclude(p => p.Pacote);

            return View(await agendamentos.ToListAsync());
        }

        // GET: Agendamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var agendamento = await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Servico)
                .Include(a => a.PacoteCliente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (agendamento == null) return NotFound();

            return View(agendamento);
        }

        // GET: Agendamentos/Create
        public IActionResult Create()
        {
            CarregarViewBags(); 
            return View();
        }

        // POST: Agendamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create([Bind("Id,ClienteId,ServicoId,PacoteClienteId,DataHoraInicio,DataHoraFim,Status,StatusPagamento,Observacao,LembreteEnviado")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            CarregarViewBags(agendamento);
            return View(agendamento);
        }

        // GET: Agendamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null) return NotFound();

            CarregarViewBags(agendamento); 
            return View(agendamento);
        }

        // POST: Agendamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ClienteId,ServicoId,PacoteClienteId,DataHoraInicio,DataHoraFim,Status,StatusPagamento,Observacao,LembreteEnviado")] Agendamento agendamento)
        {
            if (id != agendamento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoExists(agendamento.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            CarregarViewBags(agendamento);
            return View(agendamento);
        }

        // GET: Agendamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var agendamento = await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Servico)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (agendamento == null) return NotFound();

            return View(agendamento);
        }

        // POST: Agendamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(int? id)
        {
            return _context.Agendamentos.Any(e => e.Id == id);
        }

        
        private void CarregarViewBags(Agendamento agendamento = null)
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", agendamento?.ClienteId);
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "Id", "Nome", agendamento?.ServicoId);

            
            var pacotes = _context.PacotesClientes
                .Include(p => p.Pacote)
                .Where(p => p.Status == StatusPacote.Ativo)
                .Select(p => new { Id = p.Id, Descricao = p.Pacote.Nome + " (Restam: " + p.SessoesRestantes + " sessões)" })
                .ToList();

            ViewData["PacoteClienteId"] = new SelectList(pacotes, "Id", "Descricao", agendamento?.PacoteClienteId);
        }
    }
}