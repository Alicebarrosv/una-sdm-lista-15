using GreenDriveApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenDriveApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Bateria> Baterias { get; set; }
        public DbSet<EstacaoCarga> EstacoesCarga { get; set; }
        public DbSet<OrdemReciclagem> OrdensReciclagem { get; set; }
        public DbSet<RegistroTelemetria> RegistrosTelemetria { get; set; }
        
    }
}