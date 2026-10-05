using System.ComponentModel.DataAnnotations;

namespace GestaoNailDesigner.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public TipoTransacao Tipo { get; set; } 
    }
}