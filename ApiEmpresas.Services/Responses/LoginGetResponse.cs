namespace ApiEmpresas.Services.Responses
{
    /// <summary>
    /// Modelo de dados para o retorno de dados de login de usuário na API
    /// </summary>
    public class LoginGetResponse
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public DateTime DataHoraAcesso { get; set; }
        public string AccessToken { get; set; }
        public DateTime DataHoraExpiracao { get; set; }
    }
}