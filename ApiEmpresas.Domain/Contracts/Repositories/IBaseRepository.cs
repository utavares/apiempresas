using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Domain.Contracts.Repositories
{
    /// <summary>
    /// Interface genérica para operações de repositório
    /// </summary>
    /// <typeparam name="TEntity">Tipo genérico de entidade</typeparam>
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        List<TEntity> GetAll();
        List<TEntity> GetAll(Func<TEntity, bool> where);

        TEntity Get(Guid id);
        TEntity Get(Func<TEntity, bool> where);

        int Count(Func<TEntity, bool> where);
    }
}