using System.ComponentModel.DataAnnotations;

namespace GestaoNailDesigner.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A duração é obrigatória.")]
        public int DuracaoMinutos { get; set; }

        public bool Ativo { get; set; } = true;
    }
}