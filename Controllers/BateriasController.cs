using GreenDriveApi.Data;
using GreenDriveApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenDriveApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BateriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BateriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bateria>> GetBateria(int id)
        {
            var bateria = await _context.Baterias.FindAsync(id);
            if (bateria == null)
            {
                return NotFound();
            }

            return bateria;
        }

        [HttpPatch("{id}/saude")]
        public async Task<IActionResult> AtualizarSaude(int id, [FromBody] AtualizacaoSaudeDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados de atualização de saúde são obrigatórios.");
            }

            var bateria = await _context.Baterias.FindAsync(id);
            if (bateria == null)
            {
                return NotFound();
            }

            if (dto.SaudeBateria < 0 || dto.SaudeBateria > 100)
            {
                return BadRequest("O valor de saúde da bateria deve estar entre 0 e 100.");
            }

            if (bateria.SaudeBateria <= 10 && dto.SaudeBateria > bateria.SaudeBateria)
            {
                return Conflict("Atualização negada. Bateria inativa não pode ter saúde aumentada por suspeita de fraude de dados.");
            }

            bateria.SaudeBateria = dto.SaudeBateria;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class AtualizacaoSaudeDto
        {
            public int SaudeBateria { get; set; }
        }
    }
}