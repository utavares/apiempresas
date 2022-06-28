using ApiEmpresas.Domain.Contracts.Reports;
using ApiEmpresas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Domain.Contracts.Services
{
    /// <summary>
    /// Interface para operações de negócio com a entidade Funcionário
    /// </summary>
    public interface IFuncionarioDomainService : IBaseDomainService<Funcionario>
    {
        byte[] GetReport(List<Funcionario> funcionarios, ReportType formato);
    }
}