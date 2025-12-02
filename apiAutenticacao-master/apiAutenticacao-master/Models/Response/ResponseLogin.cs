namespace apiAutenticacao.Models.Response
{
    public class ResponseLogin
    {
        public bool Erro { get; set; }
        public string Mesage { get; set; } = string.Empty;
        public Usuario? Usuario { get; set; } = new Usuario();
    }
}
