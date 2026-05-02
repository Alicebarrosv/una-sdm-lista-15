using GreenDriveApi.Data;
using GreenDriveApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenDriveApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroTelemetriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistroTelemetriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<RegistroTelemetria>> PostRegistroTelemetria(RegistroTelemetria registro)
        {
            var bateria = await _context.Baterias.FindAsync(registro.BateriaId);
            if (bateria == null)
            {
                return NotFound($"Bateria com ID {registro.BateriaId} não encontrada.");
            }

            if (registro.Temperatura > 85)
            {
                Console.WriteLine($"ALERTA DE SEGURANÇA: Risco térmico detectado na bateria {bateria.NumeroSerie}! Registro bloqueado para investigação.");
                return BadRequest("Temperatura acima do limite de segurança. Registro bloqueado para investigação.");
            }

            registro.DataLeitura = DateTime.Now;
            _context.RegistrosTelemetria.Add(registro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistroTelemetria), new { id = registro.Id }, registro);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroTelemetria>> GetRegistroTelemetria(int id)
        {
            var registro = await _context.RegistrosTelemetria.FindAsync(id);
            if (registro == null)
            {
                return NotFound();
            }

            return registro;
        }
    }
}