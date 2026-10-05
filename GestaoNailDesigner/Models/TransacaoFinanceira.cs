using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoNailDesigner.Models
{
    public class TransacaoFinanceira
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(200)]
        public string Descricao { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }

        [Required]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(50)]
        public string FormaPagamento { get; set; }

        public int? AgendamentoId { get; set; }
        [ForeignKey("AgendamentoId")]
        public Agendamento? Agendamento { get; set; }

        public int? PacoteClienteId { get; set; }
        [ForeignKey("PacoteClienteId")]
        public PacoteCliente? PacoteCliente { get; set; }
    }
}