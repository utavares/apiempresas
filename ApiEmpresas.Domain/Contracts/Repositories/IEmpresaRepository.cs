using ApiEmpresas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Domain.Contracts.Repositories
{
    /// <summary>
    /// Contrato de repositório para a entidade Empresa
    /// </summary>
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {

    }
}