using ApiEmpresas.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Domain.Entities
{
    /// <summary>
    /// Entidade de domínio
    /// </summary>
    public class Usuario
    {
        #region Atributos

        private Guid _idUsuario;
        private string _nome;
        private string _login;
        private string _senha;

        #endregion

        #region Propriedades

        public Guid IdUsuario
        {
            get => _idUsuario;
            set
            {
                if (value == null || value == Guid.Empty)
                    throw new ArgumentException("Id do Usuário é obrigatório.");

                _idUsuario = value;
            }
        }

        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Nome do usuário é obrigatório.");

                else if (value.Trim().Length < 6 || value.Trim().Length > 150)
                    throw new ArgumentException("Nome do usuário deve ter de 6 a 150 caracteres.");

                _nome = value;
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Login do usuário é obrigatório.");

                else if (value.Trim().Length < 6 || value.Trim().Length > 20)
                    throw new ArgumentException("Login do usuário deve ter de 6 a 20 caracteres.");

                _login = value;
            }
        }

        public string Senha
        {
            get => _senha;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Senha do usuário é obrigatória.");

                else if (value.Trim().Length < 8 || value.Trim().Length > 20)
                    throw new ArgumentException("Senha do usuário deve ter de 8 a 20 caracteres.");

                else if (!value.Any(char.IsUpper))
                    throw new ArgumentException("Senha do usuário deve ter pelo menos 1 letra maiúscula.");

                else if (!value.Any(char.IsLower))
                    throw new ArgumentException("Senha do usuário deve ter pelo menos 1 letra minúscula.");

                else if (!value.Any(char.IsDigit))
                    throw new ArgumentException("Senha do usuário deve ter pelo menos 1 dígito numérico.");

                _senha = MD5Helper.Encrypt(value);
            }
        }

        #endregion
    }
}