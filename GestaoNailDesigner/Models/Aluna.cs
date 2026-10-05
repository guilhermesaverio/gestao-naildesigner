using System.ComponentModel.DataAnnotations;

namespace GestaoNailDesigner.Models
{
    public class Aluna
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone/WhatsApp é obrigatório.")]
        [MaxLength(20)]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [MaxLength(100)]
        public string? Email { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}