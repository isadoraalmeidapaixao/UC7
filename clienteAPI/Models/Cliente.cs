using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace clienteAPI.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é um dado obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O email é um dado obrigatório")]
        [EmailAddress(ErrorMessage = "O email informado não é válido")]
        [StringLength(100, ErrorMessage = "O email não pode ter mais de 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        public string Telefone { get; set; } = string.Empty;
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
