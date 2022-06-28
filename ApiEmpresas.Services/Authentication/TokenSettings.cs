namespace ApiEmpresas.Services.Authentication
{
    /// <summary>
    /// Classe para fazer a captura das configurações do 'appsettings.json'
    /// </summary>
    public class TokenSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationInHours { get; set; }
    }
}