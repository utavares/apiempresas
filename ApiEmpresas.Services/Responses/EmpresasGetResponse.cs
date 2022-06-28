namespace ApiEmpresas.Services.Responses
{
    /// <summary>
    /// Modelo de dados para o retorno de dados de empresa na API
    /// </summary>
    public class EmpresasGetResponse
    {
        public Guid IdEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
    }
}