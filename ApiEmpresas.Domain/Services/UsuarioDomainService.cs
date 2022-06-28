using ApiEmpresas.Domain.Contracts.Repositories;
using ApiEmpresas.Domain.Contracts.Services;
using ApiEmpresas.Domain.Entities;
using ApiEmpresas.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //atributo
        private readonly IUsuarioRepository _usuarioRepository;

        //construtor para injeção de dependência (inicialização)
        public UsuarioDomainService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void Add(Usuario entity)
        {
            #region Regra: O login deve ser único por usuário

            if (_usuarioRepository.Get(u => u.Login.Equals(entity.Login)) != null)
                throw new ArgumentException("O login informado já está cadastrado, tente outro.");

            #endregion

            _usuarioRepository.Add(entity);
        }

        public void Update(Usuario entity)
        {
            #region Regra: O Usuário deve existir no sistema

            if (_usuarioRepository.Get(entity.IdUsuario) == null)
                throw new ArgumentException("O Usuário informado não foi encontrado, verifique o ID.");

            #endregion

            #region Regra: O login não pode ser igual ao de outro usuário já cadastrado

            if (_usuarioRepository.Get(u => u.Login.Equals(entity.Login) && u.IdUsuario != entity.IdUsuario) != null)
                throw new ArgumentException("O login informado já está cadastrado para outro usuário, tente outro.");

            #endregion

            _usuarioRepository.Update(entity);
        }

        public void Delete(Usuario entity)
        {
            #region Regra: O Usuário deve existir no sistema

            if (_usuarioRepository.Get(entity.IdUsuario) == null)
                throw new ArgumentException("O Usuário informado não foi encontrado, verifique o ID.");

            #endregion

            _usuarioRepository.Delete(entity);
        }

        public List<Usuario> GetAll()
        {
            return _usuarioRepository.GetAll();
        }

        public Usuario Get(Guid id)
        {
            return _usuarioRepository.Get(id);
        }

        public Usuario Get(string login, string senha)
        {
            return _usuarioRepository.Get(u => u.Login.Equals(login)
                                            && u.Senha.Equals(MD5Helper.Encrypt(senha)));
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }
    }
}