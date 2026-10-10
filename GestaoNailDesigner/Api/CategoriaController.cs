using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoNailDesigner.Data;
using GestaoNailDesigner.Models;

namespace GestaoNailDesigner.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return Ok(categorias);
        }
    }
}