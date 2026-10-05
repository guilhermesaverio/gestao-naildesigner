using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoNailDesigner.Models
{
    public class Pacote
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do pacote é obrigatório.")]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Selecione o serviço base do pacote.")]
        public int ServicoId { get; set; }
        [ForeignKey("ServicoId")]
        public Servico? Servico { get; set; }

        [Required]
        public int QuantidadeSessoes { get; set; }

        [Required]
        public decimal ValorTotal { get; set; }

        public bool Ativo { get; set; } = true;
    }
}