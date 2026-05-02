using GreenDriveApi.Data;
using GreenDriveApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenDriveApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdemReciclagensController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdemReciclagensController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<OrdemReciclagem>> PostOrdemReciclagem(OrdemReciclagem ordem)
        {
            var bateria = await _context.Baterias.FindAsync(ordem.BateriaId);
            if (bateria == null)
            {
                return NotFound($"Bateria com ID {ordem.BateriaId} não encontrada.");
            }

            var estacao = await _context.EstacoesCarga.FindAsync(ordem.EstacaoId);
            if (estacao == null)
            {
                return NotFound($"Estação com ID {ordem.EstacaoId} não encontrada.");
            }

            if (bateria.SaudeBateria > 60)
            {
                return BadRequest("Bateria com saúde superior a 60%. Encaminhe para o programa de Reuso Doméstico (Second Life) em vez de reciclagem.");
            }

            if (string.Equals(estacao.TipoCarga, "Ultra-Rapida", StringComparison.OrdinalIgnoreCase))
            {
                ordem.CustoProcessamento += 250m;
            }

            _context.OrdensReciclagem.Add(ordem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrdemReciclagem), new { id = ordem.Id }, ordem);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemReciclagem>> GetOrdemReciclagem(int id)
        {
            var ordem = await _context.OrdensReciclagem
                .Include(o => o.Bateria)
                .Include(o => o.Estacao)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (ordem == null)
            {
                return NotFound();
            }

            return ordem;
        }
    }
}