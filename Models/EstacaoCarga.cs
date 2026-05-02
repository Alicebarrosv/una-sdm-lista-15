namespace GreenDriveApi.Models
{
    public class EstacaoCarga
    {
        public int Id { get; set; }
        public string Localizacao { get; set; }
        public string TipoCarga { get; set; }
        public double CargaDisponiveisKW { get; set; }

        public ICollection<OrdemReciclagem> OrdensReciclagem { get; set; }


    }
}