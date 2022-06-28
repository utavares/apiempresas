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
    /// Interface para operações de negócio com a entidade Empresa
    /// </summary>
    public interface IEmpresaDomainService : IBaseDomainService<Empresa>
    {
        byte[] GetReport(List<Empresa> empresas, ReportType formato);
    }
}