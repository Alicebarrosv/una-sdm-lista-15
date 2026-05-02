using System.ComponentModel.DataAnnotations.Schema;

namespace GreenDriveApi.Models
{
    public class OrdemReciclagem
    {
        public int Id { get; set; }
        [ForeignKey("Bateria")]
        public int BateriaId { get; set; }
        public int EstacaoId { get; set; }
        public string Prioridade { get; set; }
        public decimal CustoProcessamento { get; set; }

        public Bateria Bateria { get; set; }
        public EstacaoCarga Estacao { get; set; }

    }
}