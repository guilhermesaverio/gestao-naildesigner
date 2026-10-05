using System.ComponentModel.DataAnnotations;

namespace GestaoNailDesigner.Models
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do curso é obrigatório.")]
        [MaxLength(150)]
        public string Nome { get; set; }

        [MaxLength(500)]
        public string? Descricao { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public int CargaHorariaHoras { get; set; }

        public bool Ativo { get; set; } = true;
    }
}