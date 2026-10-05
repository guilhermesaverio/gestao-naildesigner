using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoNailDesigner.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [Required]
        public int ServicoId { get; set; }
        [ForeignKey("ServicoId")]
        public Servico? Servico { get; set; }

        public int? PacoteClienteId { get; set; }
        [ForeignKey("PacoteClienteId")]
        public PacoteCliente? PacoteCliente { get; set; }

        [Required]
        public DateTime DataHoraInicio { get; set; }

        [Required]
        public DateTime DataHoraFim { get; set; }

        [Required]
        public StatusAgendamento Status { get; set; } = StatusAgendamento.Agendado;

        [Required]
        public StatusPagamento StatusPagamento { get; set; } = StatusPagamento.Pendente;

        [MaxLength(500)]
        public string? Observacao { get; set; }

        public bool LembreteEnviado { get; set; } = false;
    }
}