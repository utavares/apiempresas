namespace ApiEmpresas.Services.Requests
{
    /// <summary>
    /// Modelo de dados para a requisição de login de usuário
    /// </summary>
    public class LoginPostRequest
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}