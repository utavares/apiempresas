using ApiEmpresas.Domain.Contracts.Repositories;
using ApiEmpresas.Domain.Entities;
using ApiEmpresas.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Infra.Data.Repositories
{
    /// <summary>
    /// Classe de repositorio de dados para a entidade empresa
    /// </summary>
    public class EmpresaRepository : IEmpresaRepository
    {
        //atributo
        private readonly SqlServerContext _context;

        //método construtor
        public EmpresaRepository()
        {
            //inicializando o atributo
            _context = new SqlServerContext();
        }

        public void Add(Empresa entity)
        {
            _context.Empresa.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Empresa entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Empresa entity)
        {
            _context.Empresa.Remove(entity);
            _context.SaveChanges();
        }

        public List<Empresa> GetAll()
        {
            return _context.Empresa
                .AsNoTracking()
                .OrderBy(e => e.NomeFantasia)
                .ToList();
        }

        public Empresa Get(Guid id)
        {
            return _context.Empresa
                .AsNoTracking()
                .FirstOrDefault(e => e.IdEmpresa == id);
        }

        public List<Empresa> GetAll(Func<Empresa, bool> where)
        {
            return _context.Empresa
                .AsNoTracking()
                .Where(where)
                .OrderBy(e => e.NomeFantasia)
                .ToList();
        }

        public Empresa Get(Func<Empresa, bool> where)
        {
            return _context.Empresa
                .AsNoTracking()
                .FirstOrDefault(where);
        }

        public int Count(Func<Empresa, bool> where)
        {
            return _context.Empresa
                .Count(where);
        }

        public void Dispose()
        {
            //finalizando o objeto SqlServerContext
            _context.Dispose();
        }
    }
}