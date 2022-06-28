namespace ApiEmpresas.Services.Requests
{
    /// <summary>
    /// Modelo de dados para a requisição de atualização de funcionário
    /// </summary>
    public class FuncionariosPutRequest
    {
        public Guid IdFuncionario { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Matricula { get; set; }
        public DateTime DataAdmissao { get; set; }
        public Guid IdEmpresa { get; set; }
    }
}