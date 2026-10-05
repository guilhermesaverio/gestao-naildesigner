
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Models;
using GestaoNailDesigner.Data;

namespace GestaoNailDesigner.Controllers
{
    public class TransacaoFinanceiraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransacaoFinanceiraController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var transacoes = _context.TransacoesFinanceiras
                .Include(t => t.Categoria)
                .Include(t => t.Agendamento)
                .Include(t => t.PacoteCliente);

            return View(await transacoes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transacaofinanceira = await _context.TransacoesFinanceiras
                .Include(t => t.Categoria)
                .Include(t => t.Agendamento)
                .Include(t => t.PacoteCliente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (transacaofinanceira == null)
            {
                return NotFound();
            }

            return View(transacaofinanceira);
        }

        public IActionResult Create()
        {
            CarregarViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Valor,CategoriaId,Data,FormaPagamento,AgendamentoId,PacoteClienteId")] TransacaoFinanceira transacaofinanceira)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transacaofinanceira);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            CarregarViewBags(transacaofinanceira);
            return View(transacaofinanceira);
        }

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

            CarregarViewBags(transacaofinanceira);
            return View(transacaofinanceira);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Descricao,Valor,CategoriaId,Data,FormaPagamento,AgendamentoId,PacoteClienteId")] TransacaoFinanceira transacaofinanceira)
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

            CarregarViewBags(transacaofinanceira);
            return View(transacaofinanceira);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transacaofinanceira = await _context.TransacoesFinanceiras
                .Include(t => t.Categoria)
                .Include(t => t.Agendamento)
                .Include(t => t.PacoteCliente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (transacaofinanceira == null)
            {
                return NotFound();
            }

            return View(transacaofinanceira);
        }

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

        private void CarregarViewBags(TransacaoFinanceira transacao = null)
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", transacao?.CategoriaId);
        }
    }
}