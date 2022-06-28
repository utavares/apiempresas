namespace ApiEmpresas.Services.Responses
{
    /// <summary>
    /// Modelo de dados para o retorno de dados de funcionário na API
    /// </summary>
    public class FuncionariosGetResponse
    {
        public Guid IdFuncionario { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Matricula { get; set; }
        public DateTime DataAdmissao { get; set; }

        #region Associações

        public EmpresasGetResponse Empresa { get; set; }

        #endregion
    }
}