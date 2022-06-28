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
    /// Classe de repositorio de dados para a entidade funcionário
    /// </summary>
    public class FuncionarioRepository : IFuncionarioRepository
    {
        //atributo
        private readonly SqlServerContext _context;

        //método construtor
        public FuncionarioRepository()
        {
            //inicializando o atributo
            _context = new SqlServerContext();
        }

        public void Add(Funcionario entity)
        {
            _context.Funcionario.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Funcionario entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Funcionario entity)
        {
            _context.Funcionario.Remove(entity);
            _context.SaveChanges();
        }

        public List<Funcionario> GetAll()
        {
            return _context.Funcionario
                .AsNoTracking()
                .Include(f => f.Empresa) //LEFT JOIN
                .OrderBy(f => f.Nome)
                .ToList();
        }

        public Funcionario Get(Guid id)
        {
            return _context.Funcionario
                .AsNoTracking()
                .Include(f => f.Empresa) //LEFT JOIN
                .FirstOrDefault(f => f.IdFuncionario == id);
        }

        public List<Funcionario> GetAll(Func<Funcionario, bool> where)
        {
            return _context.Funcionario
                .AsNoTracking()
                .Include(f => f.Empresa) //LEFT JOIN
                .Where(where)
                .OrderBy(f => f.Nome)
                .ToList();
        }

        public Funcionario Get(Func<Funcionario, bool> where)
        {
            return _context.Funcionario
                .AsNoTracking()
                .Include(f => f.Empresa) //LEFT JOIN
                .FirstOrDefault(where);
        }

        public int Count(Func<Funcionario, bool> where)
        {
            return _context.Funcionario
                .Count(where);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}