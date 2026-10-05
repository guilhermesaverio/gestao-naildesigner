using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoNailDesigner.Models
{
    public class PacoteCliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [Required]
        public int PacoteId { get; set; }
        [ForeignKey("PacoteId")]
        public Pacote? Pacote { get; set; }

        [Required]
        public int SessoesRestantes { get; set; }

        public DateTime DataCompra { get; set; } = DateTime.Now;

        public StatusPacote Status { get; set; } = StatusPacote.Ativo;
    }
}