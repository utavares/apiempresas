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
    /// Classe de repositorio de dados para a entidade usuário
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        //atributo
        private readonly SqlServerContext _context;

        //construtor -> ctor + 2x[tab]
        public UsuarioRepository()
        {
            _context = new SqlServerContext();
        }

        public void Add(Usuario entity)
        {
            _context.Usuario.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Usuario entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Usuario entity)
        {
            _context.Usuario.Remove(entity);
            _context.SaveChanges();
        }

        public List<Usuario> GetAll()
        {
            return _context.Usuario
                .OrderBy(u => u.Nome)
                .ToList();
        }

        public List<Usuario> GetAll(Func<Usuario, bool> where)
        {
            return _context.Usuario
                .Where(where)
                .OrderBy(u => u.Nome)
                .ToList();
        }

        public Usuario Get(Guid id)
        {
            return _context.Usuario.Find(id);
        }

        public Usuario Get(Func<Usuario, bool> where)
        {
            return _context.Usuario
                .FirstOrDefault(where);
        }

        public int Count(Func<Usuario, bool> where)
        {
            return _context.Usuario
                .Count(where);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}