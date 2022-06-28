using ApiEmpresas.Domain.Validations;
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
    public class Empresa
    {
        #region Atributos

        private Guid _idEmpresa;
        private string _nomeFantasia;
        private string _razaoSocial;
        private string _cnpj;

        #endregion

        #region Propriedades

        public Guid IdEmpresa
        {
            get => _idEmpresa;
            set
            {
                if (value == null || value == Guid.Empty)
                    throw new ArgumentException("Id da Empresa é obrigatório.");

                _idEmpresa = value;
            }
        }

        public string NomeFantasia
        {
            get => _nomeFantasia;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Nome fantasia é obrigatório.");

                else if (value.Trim().Length < 6 || value.Trim().Length > 150)
                    throw new ArgumentException("Nome fantasia deve ter de 6 a 150 caracteres.");

                _nomeFantasia = value;
            }
        }

        public string RazaoSocial
        {
            get => _razaoSocial;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Razão social é obrigatório.");

                else if (value.Trim().Length < 6 || value.Trim().Length > 150)
                    throw new ArgumentException("Razão social deve ter de 6 a 150 caracteres.");

                _razaoSocial = value;
            }
        }

        public string Cnpj
        {
            get => _cnpj;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("CNPJ é obrigatório.");

                else if (!CnpjValidation.IsValid(value))
                    throw new ArgumentException("CNPJ é inválido.");

                _cnpj = value;
            }
        }

        #endregion

        #region Associações

        public List<Funcionario> Funcionarios { get; set; }

        #endregion
    }
}