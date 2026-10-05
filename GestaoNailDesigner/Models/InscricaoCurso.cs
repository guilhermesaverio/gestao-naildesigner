using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoNailDesigner.Models
{
    public class InscricaoCurso
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A aluna é obrigatória.")]
        public int AlunaId { get; set; }
        [ForeignKey("AlunaId")]
        public Aluna? Aluna { get; set; }

        [Required(ErrorMessage = "O curso é obrigatório.")]
        public int CursoId { get; set; }
        [ForeignKey("CursoId")]
        public Curso? Curso { get; set; }

        public decimal? DescontoPercentual { get; set; }

        public decimal? DescontoValor { get; set; }

        [Required(ErrorMessage = "O valor final é obrigatório.")]
        public decimal ValorFinal { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        [Required]
        public StatusPagamento StatusPagamento { get; set; } = StatusPagamento.Pendente;

        public DateTime DataInscricao { get; set; } = DateTime.Now;
    }
}