using ApiEmpresas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Domain.Contracts.Services
{
    public interface IUsuarioDomainService : IBaseDomainService<Usuario>
    {
        Usuario Get(string login, string senha);
    }
}