namespace GreenDriveApi.Models
{
    public class Bateria
    {
        public int Id { get; set; }
        public string NumeroSerie { get; set; }
        public double CapacidadeKWh { get; set; }
        public int SaudeBateria { get; set; }

        public ICollection<RegistroTelemetria> RegistrosTelemetria { get; set; }
        public ICollection<OrdemReciclagem> OrdensReciclagem { get; set; }
        
    }
}