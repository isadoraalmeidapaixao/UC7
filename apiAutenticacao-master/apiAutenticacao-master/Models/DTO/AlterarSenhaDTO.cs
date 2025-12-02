using System.ComponentModel.DataAnnotations;

namespace apiAutenticacao.Models.DTO
{
    public class AlterarSenhaDTO
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "A senha atual é obrigatória.")]
        public string SenhaAtual { get; set; } = string.Empty;
        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A nova senha deve ter no mínimo 6 caracteres.")]
        public string NovaSenha { get; set; } = string.Empty;
        [Required(ErrorMessage = "A confirmação da nova senha é obrigatória.")]
        public string ConfirmarNovaSenha { get; set; } = string.Empty;
    }
}