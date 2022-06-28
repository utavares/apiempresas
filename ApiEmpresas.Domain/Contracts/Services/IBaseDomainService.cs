using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Domain.Contracts.Services
{
    /// <summary>
    /// Interface genérica para as operações do dominio
    /// </summary>
    /// <typeparam name="TEntity">Genérico para entidade</typeparam>
    public interface IBaseDomainService<TEntity> : IDisposable
        where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        List<TEntity> GetAll();
        TEntity Get(Guid id);
    }
}