namespace ApiEmpresas.Services.Requests
{
    /// <summary>
    /// Modelo de dados para a requisição de atualização de empresa
    /// </summary>
    public class EmpresasPutRequest
    {
        public Guid IdEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
    }
}